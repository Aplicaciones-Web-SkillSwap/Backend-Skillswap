using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Learning.Application.CommandServices;
using SkillSwap.Platform.Learning.Domain.Model;
using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Domain.Model.ValueObjects;
using SkillSwap.Platform.Learning.Domain.Repositories;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;

namespace SkillSwap.Platform.Learning.Application.Internal.CommandServices;

public class QuizCommandService(
    IQuizRepository quizRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : IQuizCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    public async Task<Result<Quiz>> Handle(CreateQuizCommand command, CancellationToken cancellationToken)
    {
        var quiz = new Quiz(command);
        try
        {
            await quizRepository.AddAsync(quiz, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Quiz>.Success(quiz);
        }
        catch (Exception)
        {
            return Result<Quiz>.Failure(LearningError.InternalServerError, _localizer[nameof(LearningError.InternalServerError)]);
        }
    }


    public async Task<Result<Question>> Handle(AddQuestionToQuizCommand command, CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.FindByIdAsync(command.QuizId, cancellationToken);
        if (quiz is null)
            return Result<Question>.Failure(LearningError.QuizNotFound, _localizer[nameof(LearningError.QuizNotFound)]);
        try
        {
            quiz.addQuestion(command.QuestionString, command.Answers, command.CorrectAnswer);
            quizRepository.Update(quiz);
            await unitOfWork.CompleteAsync(cancellationToken);
            
            return Result<Question>.Success(quiz.Questions.Last());
        }
        catch (Exception ex)
        {
            return Result<Question>.Failure(LearningError.DatabaseError, ex.Message);
        }
    }

    public async Task<Result> Handle(UpdateQuestionInQuizCommand command, CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.FindByIdAsync(command.QuizId, cancellationToken);
        if (quiz is null)
            return Result.Failure(LearningError.QuizNotFound, _localizer[nameof(LearningError.QuizNotFound)]);

        try
        {
            quiz.UpdateQuestion(command.Index, command.QuestionString, command.Answers, command.CorrectAnswer);
            quizRepository.Update(quiz);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(LearningError.DatabaseError, _localizer[nameof(LearningError.DatabaseError)]);
        }
    }
    
    public async Task<Result> Handle(UpdateQuizInfoCommand command, CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.FindByIdAsync(command.QuizId, cancellationToken);
        if (quiz is null) return Result.Failure(LearningError.QuizNotFound, _localizer[nameof(LearningError.QuizNotFound)]);

        quiz.updateInformation(command.Course, command.Title, command.Description, quiz.Questions);
        
        quizRepository.Update(quiz);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success();
    }
    
    public async Task<Result> Handle(DeleteQuizCommand command, CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.FindByIdAsync(command.QuizId, cancellationToken);
        if (quiz is null) return Result.Failure(LearningError.QuizNotFound, _localizer[nameof(LearningError.QuizNotFound)]);

        quizRepository.Remove(quiz);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success();
    }
    
    public async Task<Result> Handle(RemoveQuestionFromQuizCommand command, CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.FindByIdAsync(command.QuizId, cancellationToken);
        if (quiz is null) return Result.Failure(LearningError.QuizNotFound, _localizer[nameof(LearningError.QuizNotFound)]);
        
        quiz.deleteQuestion(command.Index);
        
        quizRepository.Update(quiz);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success();
    }
    
}
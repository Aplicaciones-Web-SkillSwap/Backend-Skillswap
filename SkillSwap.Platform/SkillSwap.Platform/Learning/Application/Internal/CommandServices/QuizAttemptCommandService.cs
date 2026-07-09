using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Learning.Application.CommandServices;
using SkillSwap.Platform.Learning.Domain.Model;
using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Domain.Repositories;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;
using SkillSwap.Platform.Workspace.Domain.Repositories;

namespace SkillSwap.Platform.Learning.Application.Internal.CommandServices;

/// <summary>
///     QuizAttempt command service
/// </summary>
/// <remarks>
///     Scores the attempt server-side by comparing the learner's selected answers
///     against the quiz's stored correct answers, so the result cannot be tampered
///     with from the client.
/// </remarks>
/// <param name="quizAttemptRepository">
///     QuizAttempt repository
/// </param>
/// <param name="quizRepository">
///     Quiz repository
/// </param>
/// <param name="sessionRepository">
///     Session repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
/// <param name="localizer">
///     String localizer for error messages
/// </param>
public class QuizAttemptCommandService(
    IQuizAttemptRepository quizAttemptRepository,
    IQuizRepository quizRepository,
    ISessionRepository sessionRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : IQuizAttemptCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<QuizAttempt>> Handle(SubmitQuizAttemptCommand command, CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.FindByIdAsync(command.QuizId, cancellationToken);
        if (quiz is null)
            return Result<QuizAttempt>.Failure(LearningError.QuizNotFound,
                _localizer[nameof(LearningError.QuizNotFound)]);

        var session = await sessionRepository.FindByIdAsync(command.SessionId, cancellationToken);
        if (session is null)
            return Result<QuizAttempt>.Failure(LearningError.SessionNotFound,
                _localizer[nameof(LearningError.SessionNotFound)]);

        if (session.SessionLearnerId.UserId != command.LearnerId)
            return Result<QuizAttempt>.Failure(LearningError.LearnerNotSessionParticipant,
                _localizer[nameof(LearningError.LearnerNotSessionParticipant)]);

        if (command.SelectedAnswers.Count != quiz.Questions.Count)
            return Result<QuizAttempt>.Failure(LearningError.InvalidAnswerCount,
                _localizer[nameof(LearningError.InvalidAnswerCount)]);

        var score = 0;
        for (var i = 0; i < quiz.Questions.Count; i++)
            if (command.SelectedAnswers[i] == quiz.Questions[i].CorrectAnswer)
                score++;

        var attempt = new QuizAttempt(command, score, quiz.Questions.Count);
        try
        {
            await quizAttemptRepository.AddAsync(attempt, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<QuizAttempt>.Success(attempt);
        }
        catch (OperationCanceledException)
        {
            return Result<QuizAttempt>.Failure(LearningError.OperationCancelled,
                _localizer[nameof(LearningError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<QuizAttempt>.Failure(LearningError.DatabaseError,
                _localizer[nameof(LearningError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<QuizAttempt>.Failure(LearningError.InternalServerError,
                _localizer[nameof(LearningError.InternalServerError)]);
        }
    }
}

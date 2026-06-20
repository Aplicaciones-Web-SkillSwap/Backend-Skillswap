using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

public static class UpdateQuestionCommandFromResourceAssembler
{
    public static UpdateQuestionInQuizCommand ToCommandFromResource(int quizId, int questionId, UpdateQuestionResource resource)
    {
        return new UpdateQuestionInQuizCommand(
            quizId, 
            questionId, 
            resource.Question, 
            resource.Answers.ToArray(), 
            resource.CorrectAnswerIndex
        );
    }
}
using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Learning.Interfaces.Rest.Transform;

public static class AddQuestionCommandFromResourceAssembler
{
    public static AddQuestionToQuizCommand ToCommandFromResource(int quizId, AddQuestionResource resource)
    {
        return new AddQuestionToQuizCommand(
            quizId,
            resource.QuestionString,
            resource.Answers,
            resource.CorrectAnswer);
    }
}
namespace SkillSwap.Platform.Learning.Interfaces.Rest.Transform;

using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

public static class UpdateQuizCommandFromResourceAssembler
{
    public static UpdateQuizInfoCommand ToCommandFromResource(int quizId, UpdateQuizInfoResource resource)
    {
        return new UpdateQuizInfoCommand(
            quizId,
            resource.Title,
            resource.Course,
            resource.Description,
            resource.status);
    }
}
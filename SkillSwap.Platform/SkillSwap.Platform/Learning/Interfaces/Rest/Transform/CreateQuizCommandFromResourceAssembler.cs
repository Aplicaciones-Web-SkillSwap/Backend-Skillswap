using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Learning.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateQuizResource" /> into a
///     <see cref="CreateQuizCommand" />.
/// </summary>
public static class CreateQuizCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateQuizResource" /> to a <see cref="CreateQuizCommand" />.
    /// </summary>
    public static CreateQuizCommand ToCommandFromResource(CreateQuizResource resource)
    {
        return new CreateQuizCommand(
            resource.Title,
            resource.Course,
            resource.Description,
            resource.TutorId);
    }
}
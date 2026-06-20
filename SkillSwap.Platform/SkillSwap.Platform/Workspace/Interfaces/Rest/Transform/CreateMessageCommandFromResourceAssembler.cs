using SkillSwap.Platform.Workspace.Domain.Model.Commands;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateMessageResource" /> into a
///     <see cref="CreateMessageCommand" />.
/// </summary>
public static class CreateMessageCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateMessageResource" /> to a <see cref="CreateMessageCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateMessageResource" /> containing the data for creating a message.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateMessageCommand" /> instance.
    /// </returns>
    public static CreateMessageCommand ToCommandFromResource(CreateMessageResource resource)
    {
        return new CreateMessageCommand(
            resource.SessionId,
            resource.SenderId,
            resource.Content,
            resource.FileUrl,
            resource.FileName,
            resource.QuizId,
            resource.QuizTitle);
    }
}
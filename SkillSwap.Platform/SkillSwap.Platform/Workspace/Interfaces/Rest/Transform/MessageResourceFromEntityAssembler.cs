using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Message" /> aggregate into a <see cref="MessageResource" />.
/// </summary>
public static class MessageResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Message" /> aggregate to its <see cref="MessageResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Message" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A <see cref="MessageResource" /> object representing the provided message.
    /// </returns>
    public static MessageResource ToResourceFromEntity(Message entity)
    {
        return new MessageResource(
            entity.Id,
            entity.SessionId,
            entity.SenderId,
            entity.Content,
            entity.FileUrl,
            entity.FileName,
            entity.QuizId,
            entity.QuizTitle,
            entity.SentAt);
    }
}
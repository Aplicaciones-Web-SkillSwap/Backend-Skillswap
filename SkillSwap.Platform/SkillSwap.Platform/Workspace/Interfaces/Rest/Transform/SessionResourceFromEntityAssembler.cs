using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Session" /> aggregate into a <see cref="SessionResource" />.
/// </summary>
public static class SessionResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Session" /> aggregate to its <see cref="SessionResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Session" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A <see cref="SessionResource" /> object representing the provided session.
    /// </returns>
    public static SessionResource ToResourceFromEntity(Session entity)
    {
        return new SessionResource(
            entity.Id,
            entity.SessionLearnerId.UserId,
            entity.SessionTutorId.UserId,
            entity.Topic,
            entity.Status,
            entity.ScheduledAt,
            entity.CourseId,
            entity.ProposedByUserId,
            entity.InitialMessage);
    }
}
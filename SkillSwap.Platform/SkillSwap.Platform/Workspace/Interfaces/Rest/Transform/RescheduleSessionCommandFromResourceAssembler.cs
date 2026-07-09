using SkillSwap.Platform.Workspace.Domain.Model.Commands;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="RescheduleSessionResource" /> into a
///     <see cref="RescheduleSessionCommand" />.
/// </summary>
public static class RescheduleSessionCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="RescheduleSessionResource" /> to a <see cref="RescheduleSessionCommand" />.
    /// </summary>
    /// <param name="sessionId">
    ///     The unique identifier of the session to reschedule.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="RescheduleSessionResource" /> containing the newly proposed date/time.
    /// </param>
    /// <param name="actorUserId">
    ///     The authenticated user's identifier, derived from the JWT.
    /// </param>
    /// <returns>
    ///     A new <see cref="RescheduleSessionCommand" /> instance.
    /// </returns>
    public static RescheduleSessionCommand ToCommandFromResource(int sessionId, RescheduleSessionResource resource, int actorUserId)
    {
        return new RescheduleSessionCommand(sessionId, resource.NewScheduledAt, actorUserId);
    }
}

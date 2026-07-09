using SkillSwap.Platform.Workspace.Domain.Model.Commands;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="UpdateSessionStatusResource" /> into a
///     <see cref="UpdateSessionStatusCommand" />.
/// </summary>
public static class UpdateSessionStatusCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="UpdateSessionStatusResource" /> to a <see cref="UpdateSessionStatusCommand" />.
    /// </summary>
    /// <param name="sessionId">
    ///     The unique identifier of the session to update.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="UpdateSessionStatusResource" /> containing the updated status.
    /// </param>
    /// <param name="actorUserId">
    ///     The authenticated caller's user id.
    /// </param>
    /// <returns>
    ///     A new <see cref="UpdateSessionStatusCommand" /> instance.
    /// </returns>
    public static UpdateSessionStatusCommand ToCommandFromResource(int sessionId, UpdateSessionStatusResource resource,
        int actorUserId)
    {
        return new UpdateSessionStatusCommand(sessionId, resource.Status, actorUserId);
    }
}
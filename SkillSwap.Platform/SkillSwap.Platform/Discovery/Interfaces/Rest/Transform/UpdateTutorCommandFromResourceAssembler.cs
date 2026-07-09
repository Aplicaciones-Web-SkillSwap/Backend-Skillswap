using SkillSwap.Platform.Discovery.Domain.Model.Commands;
using SkillSwap.Platform.Discovery.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Discovery.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="UpdateTutorResource" /> into a
///     <see cref="UpdateTutorCommand" />.
/// </summary>
public static class UpdateTutorCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="UpdateTutorResource" /> to a <see cref="UpdateTutorCommand" />.
    /// </summary>
    /// <param name="tutorId">
    ///     The unique identifier of the tutor to update.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="UpdateTutorResource" /> containing the updated data. Must not be null.
    /// </param>
    /// <param name="actorUserId">
    ///     The authenticated user's identifier, derived from the JWT.
    /// </param>
    /// <returns>
    ///     A new <see cref="UpdateTutorCommand" /> instance.
    /// </returns>
    public static UpdateTutorCommand ToCommandFromResource(int tutorId, UpdateTutorResource resource, int actorUserId)
    {
        return new UpdateTutorCommand(
            tutorId,
            resource.Bio,
            resource.Skills,
            resource.AvatarUrl,
            resource.MainSubject,
            actorUserId,
            resource.Visible);
    }
}
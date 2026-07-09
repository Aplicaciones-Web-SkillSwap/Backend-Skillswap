using SkillSwap.Platform.Workspace.Domain.Model.Commands;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateSessionResource" /> into a
///     <see cref="CreateSessionCommand" />.
/// </summary>
public static class CreateSessionCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateSessionResource" /> to a <see cref="CreateSessionCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateSessionResource" /> containing the data for creating a session.
    /// </param>
    /// <param name="learnerId">
    ///     The authenticated learner's identifier, derived from the JWT.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateSessionCommand" /> instance.
    /// </returns>
    public static CreateSessionCommand ToCommandFromResource(CreateSessionResource resource, int learnerId)
    {
        return new CreateSessionCommand(
            learnerId,
            resource.TutorId,
            resource.Topic,
            resource.ScheduledAt,
            resource.CourseId,
            resource.InitialMessage ?? string.Empty);
    }
}
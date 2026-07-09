using SkillSwap.Platform.Discovery.Domain.Model.Commands;
using SkillSwap.Platform.Discovery.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Discovery.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateTutorResource" /> into a
///     <see cref="CreateTutorCommand" />.
/// </summary>
public static class CreateTutorCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateTutorResource" /> to a <see cref="CreateTutorCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateTutorResource" /> containing the data for creating a tutor. Must not be null.
    /// </param>
    /// <param name="userId">
    ///     The authenticated user's identifier, derived from the JWT.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateTutorCommand" /> instance.
    /// </returns>
    public static CreateTutorCommand ToCommandFromResource(CreateTutorResource resource, int userId)
    {
        return new CreateTutorCommand(
            userId,
            resource.Name,
            resource.University,
            resource.Career,
            resource.Bio,
            resource.Skills,
            resource.AvatarUrl,
            resource.ExperienceYears,
            resource.MainSubject);
    }
}
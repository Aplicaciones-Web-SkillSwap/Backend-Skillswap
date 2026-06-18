using SkillSwap.Platform.Discovery.Domain.Model.Aggregates;
using SkillSwap.Platform.Discovery.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Discovery.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Tutor" /> aggregate into a <see cref="TutorResource" />.
/// </summary>
public static class TutorResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Tutor" /> aggregate to its <see cref="TutorResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Tutor" /> aggregate to convert. Must not be null.
    /// </param>
    /// <returns>
    ///     A <see cref="TutorResource" /> object representing the provided tutor.
    /// </returns>
    public static TutorResource ToResourceFromEntity(Tutor entity)
    {
        return new TutorResource(
            entity.Id,
            entity.TutorUserId.UserId,
            entity.Name,
            entity.University,
            entity.Career,
            entity.Bio,
            entity.TutorSkills.Skills,
            entity.AvatarUrl,
            entity.ExperienceYears,
            entity.MainSubject,
            entity.Rating,
            entity.ReviewCount,
            entity.Verified,
            entity.Online);
    }
}
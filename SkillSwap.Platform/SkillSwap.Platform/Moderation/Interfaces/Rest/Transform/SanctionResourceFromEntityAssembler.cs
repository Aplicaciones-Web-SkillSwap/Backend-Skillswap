using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Moderation.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Sanction" /> aggregate into a
///     <see cref="SanctionResource" />.
/// </summary>
public static class SanctionResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Sanction" /> aggregate to its <see cref="SanctionResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Sanction" /> aggregate to convert. Must not be null.
    /// </param>
    /// <returns>
    ///     A <see cref="SanctionResource" /> object representing the provided sanction.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="entity" /> is null.</exception>
    public static SanctionResource ToResourceFromEntity(Sanction entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Sanction entity cannot be null when converting to resource.");
        return new SanctionResource(
            entity.Id,
            entity.ReportId,
            entity.SanctionedUserId.UserId,
            entity.Type,
            entity.Description,
            entity.DurationDays
        );
    }
}

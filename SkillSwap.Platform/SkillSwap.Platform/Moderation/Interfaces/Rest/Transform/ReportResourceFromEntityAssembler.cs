using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Moderation.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Report" /> aggregate into a <see cref="ReportResource" />.
/// </summary>
public static class ReportResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Report" /> aggregate to its <see cref="ReportResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Report" /> aggregate to convert. Must not be null.
    /// </param>
    /// <returns>
    ///     A <see cref="ReportResource" /> object representing the provided report.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="entity" /> is null.</exception>
    public static ReportResource ToResourceFromEntity(Report entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Report entity cannot be null when converting to resource.");
        return new ReportResource(
            entity.Id,
            entity.ReporterUserId.UserId,
            entity.ReportedUserId.UserId,
            entity.ReportSessionId.Value,
            entity.Reason,
            entity.Status,
            entity.Closed,
            entity.ReportedAt
        );
    }
}
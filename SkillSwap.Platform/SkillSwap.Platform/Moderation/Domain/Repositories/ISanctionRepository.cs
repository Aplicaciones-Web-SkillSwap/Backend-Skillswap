using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Moderation.Domain.Repositories;

/// <summary>
///     Sanction repository interface
/// </summary>
public interface ISanctionRepository : IBaseRepository<Sanction>
{
    /// <summary>
    ///     Find all sanctions associated with a specific report.
    /// </summary>
    /// <param name="reportId">
    ///     The unique identifier of the report.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Sanction" /> objects.
    /// </returns>
    Task<IEnumerable<Sanction>> FindByReportIdAsync(int reportId, CancellationToken cancellationToken);

    /// <summary>
    ///     Find all sanctions applied to a specific user.
    /// </summary>
    /// <param name="userId">
    ///     The unique identifier of the sanctioned user.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Sanction" /> objects.
    /// </returns>
    Task<IEnumerable<Sanction>> FindBySanctionedUserIdAsync(int userId, CancellationToken cancellationToken);
}

using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Moderation.Domain.Repositories;

/// <summary>
///     Report repository interface
/// </summary>
public interface IReportRepository : IBaseRepository<Report>
{
    /// <summary>
    ///     Find all reports targeting a specific reported user
    /// </summary>
    /// <param name="reportedUserId">
    ///     The unique identifier of the reported user.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Report" /> objects.
    /// </returns>
    Task<IEnumerable<Report>> FindByReportedUserIdAsync(int reportedUserId, CancellationToken cancellationToken);

    /// <summary>
    ///     Checks whether a pending report already exists between two users.
    /// </summary>
    /// <param name="reporterUserId">The reporter user id.</param>
    /// <param name="reportedUserId">The reported user id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if a pending report already exists; otherwise, false.</returns>
    Task<bool> ExistsPendingReportAsync(int reporterUserId, int reportedUserId, CancellationToken cancellationToken);
}

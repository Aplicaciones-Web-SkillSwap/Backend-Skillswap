using SkillSwap.Platform.Reputation.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Reputation.Domain.Repositories;

/// <summary>
///     Review repository interface
/// </summary>
public interface IReviewRepository : IBaseRepository<Review>
{
    /// <summary>
    ///     Find all reviews for a specific tutor
    /// </summary>
    /// <param name="tutorId">
    ///     The unique identifier of the tutor
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Review" /> objects
    /// </returns>
    Task<IEnumerable<Review>> FindByTutorIdAsync(int tutorId, CancellationToken cancellationToken);

    /// <summary>
    ///     Checks whether a review already exists for a given session by a given reviewer
    /// </summary>
    /// <param name="reviewerId">
    ///     The unique identifier of the reviewer
    /// </param>
    /// <param name="sessionId">
    ///     The unique identifier of the session
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     True if a review already exists; otherwise false
    /// </returns>
    Task<bool> ExistsByReviewerAndSessionAsync(int reviewerId, int sessionId, CancellationToken cancellationToken);
}
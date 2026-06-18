using SkillSwap.Platform.Reputation.Domain.Model.Aggregates;
using SkillSwap.Platform.Reputation.Domain.Model.Queries;

namespace SkillSwap.Platform.Reputation.Application.QueryServices;

/// <summary>
///     Review query service interface
/// </summary>
public interface IReviewQueryService
{
    /// <summary>
    ///     Handle get all reviews query
    /// </summary>
    /// <param name="query">The <see cref="GetAllReviewsQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Review" /> objects
    /// </returns>
    Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get review by id query
    /// </summary>
    /// <param name="query">The <see cref="GetReviewByIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Review" /> if found; otherwise null
    /// </returns>
    Task<Review?> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get reviews by tutor id query
    /// </summary>
    /// <param name="query">The <see cref="GetReviewsByTutorIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Review" /> objects
    /// </returns>
    Task<IEnumerable<Review>> Handle(GetReviewsByTutorIdQuery query, CancellationToken cancellationToken);
}
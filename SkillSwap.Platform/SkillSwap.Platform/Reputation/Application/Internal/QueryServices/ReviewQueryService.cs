using SkillSwap.Platform.Reputation.Application.QueryServices;
using SkillSwap.Platform.Reputation.Domain.Model.Aggregates;
using SkillSwap.Platform.Reputation.Domain.Model.Queries;
using SkillSwap.Platform.Reputation.Domain.Repositories;

namespace SkillSwap.Platform.Reputation.Application.Internal.QueryServices;

/// <summary>
///     Review query service
/// </summary>
/// <param name="reviewRepository">
///     Review repository
/// </param>
public class ReviewQueryService(IReviewRepository reviewRepository) : IReviewQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query, CancellationToken cancellationToken)
    {
        return await reviewRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Review?> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken)
    {
        return await reviewRepository.FindByIdAsync(query.ReviewId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Review>> Handle(GetReviewsByTutorIdQuery query, CancellationToken cancellationToken)
    {
        return await reviewRepository.FindByTutorIdAsync(query.TutorId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<(double AverageRating, int ReviewCount)> Handle(GetTutorReputationSummaryQuery query,
        CancellationToken cancellationToken)
    {
        var reviews = (await reviewRepository.FindByTutorIdAsync(query.TutorId, cancellationToken)).ToList();
        if (reviews.Count == 0) return (0.0, 0);

        var average = reviews.Average(r => r.Rating);
        return (average, reviews.Count);
    }
}
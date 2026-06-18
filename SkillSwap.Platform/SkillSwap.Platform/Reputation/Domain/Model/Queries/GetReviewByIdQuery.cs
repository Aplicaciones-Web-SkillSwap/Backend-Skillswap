namespace SkillSwap.Platform.Reputation.Domain.Model.Queries;

/// <summary>
///     Get review by id query
/// </summary>
/// <param name="ReviewId">
///     The unique identifier of the review
/// </param>
public record GetReviewByIdQuery(int ReviewId);
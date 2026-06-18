namespace SkillSwap.Platform.Reputation.Domain.Model.Queries;

/// <summary>
///     Get reviews by tutor id query
/// </summary>
/// <param name="TutorId">
///     The unique identifier of the tutor
/// </param>
public record GetReviewsByTutorIdQuery(int TutorId);
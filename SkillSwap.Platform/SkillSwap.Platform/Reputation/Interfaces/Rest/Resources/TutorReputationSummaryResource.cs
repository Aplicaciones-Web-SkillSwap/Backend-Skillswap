namespace SkillSwap.Platform.Reputation.Interfaces.Rest.Resources;

/// <summary>
///     Tutor reputation summary resource for REST API
/// </summary>
/// <param name="TutorId">
///     The unique identifier of the tutor
/// </param>
/// <param name="AverageRating">
///     The average rating across all reviews for this tutor
/// </param>
/// <param name="ReviewCount">
///     The total number of reviews for this tutor
/// </param>
public record TutorReputationSummaryResource(
    int TutorId,
    double AverageRating,
    int ReviewCount);
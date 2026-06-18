namespace SkillSwap.Platform.Reputation.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new review
/// </summary>
/// <param name="ReviewerId">
///     The unique identifier of the user who writes the review
/// </param>
/// <param name="TutorId">
///     The unique identifier of the tutor being reviewed
/// </param>
/// <param name="SessionId">
///     The unique identifier of the session this review is related to
/// </param>
/// <param name="Rating">
///     The rating given to the tutor (1 to 5)
/// </param>
/// <param name="Comment">
///     The comment describing the review
/// </param>
public record CreateReviewResource(
    int ReviewerId,
    int TutorId,
    int SessionId,
    int Rating,
    string Comment);
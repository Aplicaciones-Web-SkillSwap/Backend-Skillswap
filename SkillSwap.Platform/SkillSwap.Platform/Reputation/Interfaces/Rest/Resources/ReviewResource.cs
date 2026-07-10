namespace SkillSwap.Platform.Reputation.Interfaces.Rest.Resources;

/// <summary>
///     Review resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the review
/// </param>
/// <param name="ReviewerId">
///     The unique identifier of the user who wrote the review
/// </param>
/// <param name="TutorId">
///     The unique identifier of the session's tutor
/// </param>
/// <param name="LearnerId">
///     The unique identifier of the session's learner
/// </param>
/// <param name="SessionId">
///     The unique identifier of the session this review is related to
/// </param>
/// <param name="Rating">
///     The rating given (1 to 5)
/// </param>
/// <param name="Comment">
///     The comment describing the review
/// </param>
/// <param name="ReviewedAt">
///     The date and time the review was created
/// </param>
public record ReviewResource(
    int Id,
    int ReviewerId,
    int TutorId,
    int LearnerId,
    int SessionId,
    int Rating,
    string Comment,
    DateTime ReviewedAt);
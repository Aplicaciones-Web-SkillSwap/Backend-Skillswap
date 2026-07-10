namespace SkillSwap.Platform.Reputation.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new review
/// </summary>
/// <remarks>
///     Who is reviewing whom is resolved server-side from the session and the authenticated
///     caller, so the client only ever supplies the session and the rating/comment.
/// </remarks>
/// <param name="SessionId">
///     The unique identifier of the session this review is related to
/// </param>
/// <param name="Rating">
///     The rating given (1 to 5)
/// </param>
/// <param name="Comment">
///     The comment describing the review
/// </param>
public record CreateReviewResource(
    int SessionId,
    int Rating,
    string Comment);

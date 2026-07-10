namespace SkillSwap.Platform.Reputation.Domain.Model.Commands;

/// <summary>
///     Create Review Command
/// </summary>
/// <remarks>
///     A review can go in either direction for a completed session: the learner reviewing the
///     tutor, or the tutor reviewing the learner. TutorId and LearnerId are resolved server-side
///     from the session (never trusted from the client) once ReviewerId's participation is
///     validated, so callers only need to supply ReviewerId, SessionId, Rating and Comment.
/// </remarks>
/// <param name="ReviewerId">
///     The unique identifier of the user who writes the review
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
/// <param name="TutorId">
///     The unique identifier of the session's tutor, resolved server-side
/// </param>
/// <param name="LearnerId">
///     The unique identifier of the session's learner, resolved server-side
/// </param>
public record CreateReviewCommand(
    int ReviewerId,
    int SessionId,
    int Rating,
    string Comment,
    int TutorId = 0,
    int LearnerId = 0);

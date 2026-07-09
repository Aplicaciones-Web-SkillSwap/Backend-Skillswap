namespace SkillSwap.Platform.Reputation.Domain.Model;

public enum ReputationError
{
    None,
    ReviewNotFound,
    InvalidRating,
    DuplicateReview,
    SessionNotFound,
    SessionNotCompleted,
    ReviewerNotSessionLearner,
    TutorMismatch,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
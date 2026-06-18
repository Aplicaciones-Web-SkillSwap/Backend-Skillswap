namespace SkillSwap.Platform.Reputation.Domain.Model;

public enum ReputationError
{
    None,
    ReviewNotFound,
    InvalidRating,
    DuplicateReview,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
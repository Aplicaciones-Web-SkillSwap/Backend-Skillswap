namespace SkillSwap.Platform.Reputation.Domain.Model;

public enum ReputationError
{
    None,
    ReviewNotFound,
    InvalidRating,
    DuplicateReview,
    SessionNotFound,
    SessionNotCompleted,
    ReviewerNotSessionParticipant,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
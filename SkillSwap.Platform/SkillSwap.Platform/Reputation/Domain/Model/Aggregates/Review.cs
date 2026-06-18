using SkillSwap.Platform.Reputation.Domain.Model.Commands;
using SkillSwap.Platform.Reputation.Domain.Model.ValueObjects;

namespace SkillSwap.Platform.Reputation.Domain.Model.Aggregates;

/// <summary>
///     Review Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Review aggregate root.
///     It contains the properties and methods to manage a review left by a learner about a tutor.
/// </remarks>
public partial class Review
{
    public Review()
    {
        ReviewerUserId = new ReviewerId();
        ReviewedTutorId = new ReviewedTutorId();
        SessionId = 0;
        Rating = 0;
        Comment = string.Empty;
        ReviewedAt = DateTime.UtcNow;
    }

    public Review(CreateReviewCommand command)
    {
        ReviewerUserId = new ReviewerId(command.ReviewerId);
        ReviewedTutorId = new ReviewedTutorId(command.TutorId);
        SessionId = command.SessionId;
        Rating = command.Rating;
        Comment = command.Comment;
        ReviewedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public ReviewerId ReviewerUserId { get; private set; }
    public ReviewedTutorId ReviewedTutorId { get; private set; }
    public int SessionId { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public DateTime ReviewedAt { get; private set; }
}
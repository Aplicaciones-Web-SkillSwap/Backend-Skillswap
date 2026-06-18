using SkillSwap.Platform.Reputation.Domain.Model.Aggregates;
using SkillSwap.Platform.Reputation.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Reputation.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Review" /> aggregate into a <see cref="ReviewResource" />.
/// </summary>
public static class ReviewResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Review" /> aggregate to its <see cref="ReviewResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Review" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A <see cref="ReviewResource" /> object representing the provided review.
    /// </returns>
    public static ReviewResource ToResourceFromEntity(Review entity)
    {
        return new ReviewResource(
            entity.Id,
            entity.ReviewerUserId.UserId,
            entity.ReviewedTutorId.TutorId,
            entity.SessionId,
            entity.Rating,
            entity.Comment,
            entity.ReviewedAt);
    }
}
using SkillSwap.Platform.Reputation.Domain.Model.Commands;
using SkillSwap.Platform.Reputation.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Reputation.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateReviewResource" /> into a
///     <see cref="CreateReviewCommand" />.
/// </summary>
public static class CreateReviewCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateReviewResource" /> to a <see cref="CreateReviewCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateReviewResource" /> containing the data for creating a review.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateReviewCommand" /> instance.
    /// </returns>
    public static CreateReviewCommand ToCommandFromResource(CreateReviewResource resource)
    {
        return new CreateReviewCommand(
            resource.ReviewerId,
            resource.TutorId,
            resource.SessionId,
            resource.Rating,
            resource.Comment);
    }
}
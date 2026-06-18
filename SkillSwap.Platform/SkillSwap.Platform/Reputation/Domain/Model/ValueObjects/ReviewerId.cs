namespace SkillSwap.Platform.Reputation.Domain.Model.ValueObjects;

/// <summary>
///     Reviewer identifier value object
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user who writes the review
/// </param>
public record ReviewerId(int UserId)
{
    public ReviewerId() : this(0)
    {
    }
}
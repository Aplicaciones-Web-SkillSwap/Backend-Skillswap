namespace SkillSwap.Platform.Reputation.Domain.Model.ValueObjects;

/// <summary>
///     Reviewed learner identifier value object
/// </summary>
/// <param name="LearnerId">
///     The unique identifier of the learner being reviewed
/// </param>
public record ReviewedLearnerId(int LearnerId)
{
    public ReviewedLearnerId() : this(0)
    {
    }
}

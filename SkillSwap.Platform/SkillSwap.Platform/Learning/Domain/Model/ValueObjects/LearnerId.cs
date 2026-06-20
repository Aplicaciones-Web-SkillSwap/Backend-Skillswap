namespace SkillSwap.Platform.Learning.Domain.Model.ValueObjects;

/// <summary>
///     Learner identifier value object
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user who is a learner
/// </param>
public record LearnerId(int UserId)
{
    public LearnerId() : this(0)
    {
    }
}
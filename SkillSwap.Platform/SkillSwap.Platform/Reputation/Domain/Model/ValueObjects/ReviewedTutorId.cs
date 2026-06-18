namespace SkillSwap.Platform.Reputation.Domain.Model.ValueObjects;

/// <summary>
///     Reviewed tutor identifier value object
/// </summary>
/// <param name="TutorId">
///     The unique identifier of the tutor being reviewed
/// </param>
public record ReviewedTutorId(int TutorId)
{
    public ReviewedTutorId() : this(0)
    {
    }
}
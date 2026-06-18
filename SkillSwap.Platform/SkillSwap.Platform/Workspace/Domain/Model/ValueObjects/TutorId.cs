namespace SkillSwap.Platform.Workspace.Domain.Model.ValueObjects;

/// <summary>
///     Tutor identifier value object
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user who is a tutor
/// </param>
public record TutorId(int UserId)
{
    public TutorId() : this(0)
    {
    }
}
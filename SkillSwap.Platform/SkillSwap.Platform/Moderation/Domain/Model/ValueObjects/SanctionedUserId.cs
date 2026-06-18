namespace SkillSwap.Platform.Moderation.Domain.Model.ValueObjects;

/// <summary>
///     Sanctioned user id value object
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user receiving the sanction
/// </param>
public record SanctionedUserId(int UserId)
{
    public SanctionedUserId() : this(0)
    {
    }
}

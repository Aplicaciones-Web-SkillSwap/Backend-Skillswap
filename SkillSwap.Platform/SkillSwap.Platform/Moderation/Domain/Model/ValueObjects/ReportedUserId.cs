namespace SkillSwap.Platform.Moderation.Domain.Model.ValueObjects;

/// <summary>
///     Reported user id value object
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user who is being reported
/// </param>
public record ReportedUserId(int UserId)
{
    public ReportedUserId() : this(0)
    {
    }
}

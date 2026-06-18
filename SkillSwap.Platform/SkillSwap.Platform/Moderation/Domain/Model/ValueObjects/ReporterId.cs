namespace SkillSwap.Platform.Moderation.Domain.Model.ValueObjects;

/// <summary>
///     Reporter id value object
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user who creates the report
/// </param>
public record ReporterId(int UserId)
{
    public ReporterId() : this(0)
    {
    }
}

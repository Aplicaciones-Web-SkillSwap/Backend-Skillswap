namespace SkillSwap.Platform.Moderation.Domain.Model.ValueObjects;

/// <summary>
///     Session identifier value object
/// </summary>
/// <param name="Value">
///     The unique identifier of the session related to the report
/// </param>
public record SessionId(int Value)
{
    public SessionId() : this(0)
    {
    }
}
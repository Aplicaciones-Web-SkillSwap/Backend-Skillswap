namespace SkillSwap.Platform.Payments.Domain.Model.ValueObjects;

/// <summary>
///     Owner identifier value object
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user who owns the wallet
/// </param>
public record OwnerId(int UserId)
{
    public OwnerId() : this(0)
    {
    }
}
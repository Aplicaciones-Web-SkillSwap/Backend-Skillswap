namespace SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

/// <summary>
///     Payment method resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the payment method
/// </param>
/// <param name="UserId">
///     The unique identifier of the user this payment method belongs to
/// </param>
/// <param name="Type">
///     The type of payment method: "card", "bank" or "yape"
/// </param>
/// <param name="DisplayLabel">
///     A masked, display-safe summary of the payment method
/// </param>
/// <param name="CreatedAt">
///     The date and time the payment method was saved
/// </param>
public record PaymentMethodResource(
    int Id,
    int UserId,
    string Type,
    string DisplayLabel,
    DateTime CreatedAt);

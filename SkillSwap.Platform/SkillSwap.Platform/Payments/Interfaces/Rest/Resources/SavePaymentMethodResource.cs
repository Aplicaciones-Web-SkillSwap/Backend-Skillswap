namespace SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

/// <summary>
///     Resource for saving (creating or replacing) the authenticated user's payment method
/// </summary>
/// <param name="Type">
///     The type of payment method: "card", "bank" or "yape"
/// </param>
/// <param name="DisplayLabel">
///     A masked, display-safe summary of the payment method (e.g. "Visa •••• 4242")
/// </param>
public record SavePaymentMethodResource(string Type, string DisplayLabel);

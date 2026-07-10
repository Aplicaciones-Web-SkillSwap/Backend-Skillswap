namespace SkillSwap.Platform.Payments.Domain.Model.Commands;

/// <summary>
///     Save Payment Method Command
/// </summary>
/// <remarks>
///     A user can have at most one saved payment method; saving a new one replaces the existing
///     one. Only a masked/display-safe label is stored — never raw card numbers, CVVs, or full
///     bank account numbers, since this is a fully simulated payment mechanism.
/// </remarks>
/// <param name="UserId">
///     The unique identifier of the user this payment method belongs to
/// </param>
/// <param name="Type">
///     The type of payment method: "card", "bank" or "yape"
/// </param>
/// <param name="DisplayLabel">
///     A masked, display-safe summary of the payment method (e.g. "Visa •••• 4242")
/// </param>
public record SavePaymentMethodCommand(int UserId, string Type, string DisplayLabel);

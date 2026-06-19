namespace SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

/// <summary>
///     Resource for adding funds to a wallet
/// </summary>
/// <param name="Amount">
///     The amount of funds to add
/// </param>
public record AddFundsResource(decimal Amount);
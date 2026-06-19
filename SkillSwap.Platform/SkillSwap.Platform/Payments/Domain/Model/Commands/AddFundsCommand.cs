namespace SkillSwap.Platform.Payments.Domain.Model.Commands;

/// <summary>
///     Add Funds Command
/// </summary>
/// <param name="WalletId">
///     The unique identifier of the wallet to add funds to
/// </param>
/// <param name="Amount">
///     The amount of funds to add
/// </param>
public record AddFundsCommand(
    int WalletId,
    decimal Amount);
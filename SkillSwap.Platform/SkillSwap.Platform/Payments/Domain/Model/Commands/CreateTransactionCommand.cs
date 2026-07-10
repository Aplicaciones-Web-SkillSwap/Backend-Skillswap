namespace SkillSwap.Platform.Payments.Domain.Model.Commands;

/// <summary>
///     Create Transaction Command
/// </summary>
/// <param name="WalletId">
///     The unique identifier of the wallet this transaction belongs to
/// </param>
/// <param name="Amount">
///     The net amount of the transaction (added to the wallet)
/// </param>
/// <param name="Type">
///     The type of the transaction (deposit, withdrawal, payment)
/// </param>
/// <param name="Description">
///     The description of the transaction
/// </param>
/// <param name="AmountSent">
///     The original gross amount sent by the payer, before any platform fee
/// </param>
/// <param name="PlatformFee">
///     The platform fee deducted from <paramref name="AmountSent" />
/// </param>
public record CreateTransactionCommand(
    int WalletId,
    decimal Amount,
    string Type,
    string Description,
    decimal AmountSent,
    decimal PlatformFee);
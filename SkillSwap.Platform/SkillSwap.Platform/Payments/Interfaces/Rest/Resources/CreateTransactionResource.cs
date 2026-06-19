namespace SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new transaction
/// </summary>
/// <param name="WalletId">
///     The unique identifier of the wallet this transaction belongs to
/// </param>
/// <param name="Amount">
///     The amount of the transaction
/// </param>
/// <param name="Type">
///     The type of the transaction (deposit, withdrawal, payment)
/// </param>
/// <param name="Description">
///     The description of the transaction
/// </param>
public record CreateTransactionResource(
    int WalletId,
    decimal Amount,
    string Type,
    string Description);
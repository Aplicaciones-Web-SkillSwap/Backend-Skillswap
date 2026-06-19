namespace SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

/// <summary>
///     Transaction resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the transaction
/// </param>
/// <param name="WalletId">
///     The unique identifier of the wallet this transaction belongs to
/// </param>
/// <param name="Amount">
///     The amount of the transaction
/// </param>
/// <param name="Type">
///     The type of the transaction
/// </param>
/// <param name="Description">
///     The description of the transaction
/// </param>
/// <param name="CreatedAt">
///     The date and time the transaction was created
/// </param>
public record TransactionResource(
    int Id,
    int WalletId,
    decimal Amount,
    string Type,
    string Description,
    DateTime CreatedAt);
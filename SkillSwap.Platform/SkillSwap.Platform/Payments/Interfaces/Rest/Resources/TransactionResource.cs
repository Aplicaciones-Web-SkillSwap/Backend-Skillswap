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
/// <param name="AmountSent">
///     The original gross amount sent by the payer, before the platform fee
/// </param>
/// <param name="PlatformFee">
///     The platform fee deducted from <paramref name="AmountSent" />
/// </param>
/// <param name="AmountReceived">
///     The net amount this transaction added to the wallet
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
    decimal AmountSent,
    decimal PlatformFee,
    decimal AmountReceived,
    string Type,
    string Description,
    DateTime CreatedAt);
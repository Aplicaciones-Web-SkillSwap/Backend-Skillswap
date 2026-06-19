namespace SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

/// <summary>
///     Wallet resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the wallet
/// </param>
/// <param name="UserId">
///     The unique identifier of the user who owns the wallet
/// </param>
/// <param name="Balance">
///     The current balance of the wallet
/// </param>
public record WalletResource(
    int Id,
    int UserId,
    decimal Balance);
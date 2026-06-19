namespace SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new wallet
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user who owns the wallet
/// </param>
public record CreateWalletResource(int UserId);
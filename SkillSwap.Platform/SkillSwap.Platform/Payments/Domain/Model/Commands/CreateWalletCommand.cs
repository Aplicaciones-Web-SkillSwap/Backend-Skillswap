namespace SkillSwap.Platform.Payments.Domain.Model.Commands;

/// <summary>
///     Create Wallet Command
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user who owns the wallet
/// </param>
public record CreateWalletCommand(int UserId);
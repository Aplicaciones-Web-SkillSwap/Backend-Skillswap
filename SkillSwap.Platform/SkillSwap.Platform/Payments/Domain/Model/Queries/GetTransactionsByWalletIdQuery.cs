namespace SkillSwap.Platform.Payments.Domain.Model.Queries;

/// <summary>
///     Get transactions by wallet id query
/// </summary>
/// <param name="WalletId">
///     The unique identifier of the wallet
/// </param>
public record GetTransactionsByWalletIdQuery(int WalletId);
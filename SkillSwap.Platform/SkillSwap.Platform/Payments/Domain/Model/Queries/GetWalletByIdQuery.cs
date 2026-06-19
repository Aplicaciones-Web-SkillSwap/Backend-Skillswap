namespace SkillSwap.Platform.Payments.Domain.Model.Queries;

/// <summary>
///     Get wallet by id query
/// </summary>
/// <param name="WalletId">
///     The unique identifier of the wallet
/// </param>
public record GetWalletByIdQuery(int WalletId);
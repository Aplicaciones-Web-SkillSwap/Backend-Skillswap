using SkillSwap.Platform.Payments.Application.QueryServices;
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Queries;
using SkillSwap.Platform.Payments.Domain.Repositories;

namespace SkillSwap.Platform.Payments.Application.Internal.QueryServices;

/// <summary>
///     Wallet query service
/// </summary>
/// <param name="walletRepository">
///     Wallet repository
/// </param>
public class WalletQueryService(IWalletRepository walletRepository) : IWalletQueryService
{
    /// <inheritdoc />
    public async Task<Wallet?> Handle(GetWalletByIdQuery query, CancellationToken cancellationToken)
    {
        return await walletRepository.FindByIdAsync(query.WalletId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Wallet?> Handle(GetWalletByUserIdQuery query, CancellationToken cancellationToken)
    {
        return await walletRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}
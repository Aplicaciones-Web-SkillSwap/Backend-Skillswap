using SkillSwap.Platform.Payments.Application.QueryServices;
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Queries;
using SkillSwap.Platform.Payments.Domain.Repositories;

namespace SkillSwap.Platform.Payments.Application.Internal.QueryServices;

/// <summary>
///     Transaction query service
/// </summary>
/// <param name="transactionRepository">
///     Transaction repository
/// </param>
public class TransactionQueryService(ITransactionRepository transactionRepository) : ITransactionQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Transaction>> Handle(GetAllTransactionsQuery query, CancellationToken cancellationToken)
    {
        return await transactionRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Transaction>> Handle(GetTransactionsByWalletIdQuery query, CancellationToken cancellationToken)
    {
        return await transactionRepository.FindByWalletIdAsync(query.WalletId, cancellationToken);
    }
}
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Payments.Domain.Repositories;

/// <summary>
///     Transaction repository interface
/// </summary>
public interface ITransactionRepository : IBaseRepository<Transaction>
{
    /// <summary>
    ///     Find all transactions for a specific wallet
    /// </summary>
    /// <param name="walletId">
    ///     The unique identifier of the wallet
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Transaction" /> objects
    /// </returns>
    Task<IEnumerable<Transaction>> FindByWalletIdAsync(int walletId, CancellationToken cancellationToken);
}
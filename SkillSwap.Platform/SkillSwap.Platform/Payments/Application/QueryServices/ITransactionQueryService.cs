using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Queries;

namespace SkillSwap.Platform.Payments.Application.QueryServices;

/// <summary>
///     Transaction query service interface
/// </summary>
public interface ITransactionQueryService
{
    /// <summary>
    ///     Handle get all transactions query
    /// </summary>
    /// <param name="query">The <see cref="GetAllTransactionsQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Transaction" /> objects
    /// </returns>
    Task<IEnumerable<Transaction>> Handle(GetAllTransactionsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get transactions by wallet id query
    /// </summary>
    /// <param name="query">The <see cref="GetTransactionsByWalletIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Transaction" /> objects
    /// </returns>
    Task<IEnumerable<Transaction>> Handle(GetTransactionsByWalletIdQuery query, CancellationToken cancellationToken);
}
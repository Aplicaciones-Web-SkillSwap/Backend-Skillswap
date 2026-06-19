using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Queries;

namespace SkillSwap.Platform.Payments.Application.QueryServices;

/// <summary>
///     Wallet query service interface
/// </summary>
public interface IWalletQueryService
{
    /// <summary>
    ///     Handle get all wallets query
    /// </summary>
    /// <param name="query">The <see cref="GetAllWalletsQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Wallet" /> objects
    /// </returns>
    Task<IEnumerable<Wallet>> Handle(GetAllWalletsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get wallet by id query
    /// </summary>
    /// <param name="query">The <see cref="GetWalletByIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Wallet" /> if found; otherwise null
    /// </returns>
    Task<Wallet?> Handle(GetWalletByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get wallet by user id query
    /// </summary>
    /// <param name="query">The <see cref="GetWalletByUserIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Wallet" /> if found; otherwise null
    /// </returns>
    Task<Wallet?> Handle(GetWalletByUserIdQuery query, CancellationToken cancellationToken);
}
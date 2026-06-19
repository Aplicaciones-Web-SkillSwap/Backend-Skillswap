using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Payments.Domain.Repositories;

/// <summary>
///     Wallet repository interface
/// </summary>
public interface IWalletRepository : IBaseRepository<Wallet>
{
    /// <summary>
    ///     Find a wallet by user id
    /// </summary>
    /// <param name="userId">
    ///     The unique identifier of the user
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Wallet" /> if found; otherwise null
    /// </returns>
    Task<Wallet?> FindByUserIdAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Checks whether a wallet already exists for a given user id
    /// </summary>
    /// <param name="userId">
    ///     The unique identifier of the user
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     True if a wallet already exists; otherwise false
    /// </returns>
    Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken);
}
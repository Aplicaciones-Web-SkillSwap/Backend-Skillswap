using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Payments.Application.CommandServices;

/// <summary>
///     Wallet command service interface
/// </summary>
public interface IWalletCommandService
{
    /// <summary>
    ///     Handle create wallet command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateWalletCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the created <see cref="Wallet" />
    /// </returns>
    Task<Result<Wallet>> Handle(CreateWalletCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle add funds command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="AddFundsCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the updated <see cref="Wallet" />
    /// </returns>
    Task<Result<Wallet>> Handle(AddFundsCommand command, CancellationToken cancellationToken);
}
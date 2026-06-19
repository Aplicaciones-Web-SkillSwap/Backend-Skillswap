using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Payments.Application.CommandServices;

/// <summary>
///     Transaction command service interface
/// </summary>
public interface ITransactionCommandService
{
    /// <summary>
    ///     Handle create transaction command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateTransactionCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the created <see cref="Transaction" />
    /// </returns>
    Task<Result<Transaction>> Handle(CreateTransactionCommand command, CancellationToken cancellationToken);
}
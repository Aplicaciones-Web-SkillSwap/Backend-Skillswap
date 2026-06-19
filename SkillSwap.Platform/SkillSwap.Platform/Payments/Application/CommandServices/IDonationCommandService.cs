using SkillSwap.Platform.Payments.Domain.Model;
using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Payments.Application.CommandServices;

/// <summary>
///     Donation command service interface
/// </summary>
public interface IDonationCommandService
{
    /// <summary>
    ///     Handle donate command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="DonateCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the <see cref="DonationResult" />
    /// </returns>
    Task<Result<DonationResult>> Handle(DonateCommand command, CancellationToken cancellationToken);
}
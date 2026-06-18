using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Moderation.Application.CommandServices;

/// <summary>
///     Report command service interface
/// </summary>
public interface IReportCommandService
{
    /// <summary>
    ///     Handle create report command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateReportCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the created <see cref="Report" />
    /// </returns>
    Task<Result<Report>> Handle(CreateReportCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle close report command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CloseReportCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the closed <see cref="Report" />
    /// </returns>
    Task<Result<Report>> Handle(CloseReportCommand command, CancellationToken cancellationToken);
}

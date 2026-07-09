using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Workspace.Application.CommandServices;

/// <summary>
///     Session command service interface
/// </summary>
public interface ISessionCommandService
{
    /// <summary>
    ///     Handle create session command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateSessionCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the created <see cref="Session" />
    /// </returns>
    Task<Result<Session>> Handle(CreateSessionCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle update session status command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="UpdateSessionStatusCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the updated <see cref="Session" />
    /// </returns>
    Task<Result<Session>> Handle(UpdateSessionStatusCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle reschedule session command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="RescheduleSessionCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the rescheduled <see cref="Session" />
    /// </returns>
    Task<Result<Session>> Handle(RescheduleSessionCommand command, CancellationToken cancellationToken);
}
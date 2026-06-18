using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Workspace.Application.CommandServices;

/// <summary>
///     Message command service interface
/// </summary>
public interface IMessageCommandService
{
    /// <summary>
    ///     Handle create message command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateMessageCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the created <see cref="Message" />
    /// </returns>
    Task<Result<Message>> Handle(CreateMessageCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle delete message command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="DeleteMessageCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result" /> indicating success or failure
    /// </returns>
    Task<Result> Handle(DeleteMessageCommand command, CancellationToken cancellationToken);
}
using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Workspace.Domain.Repositories;

/// <summary>
///     Message repository interface
/// </summary>
public interface IMessageRepository : IBaseRepository<Message>
{
    /// <summary>
    ///     Find all messages by session id
    /// </summary>
    /// <param name="sessionId">
    ///     The unique identifier of the session
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Message" /> objects
    /// </returns>
    Task<IEnumerable<Message>> FindBySessionIdAsync(int sessionId, CancellationToken cancellationToken);
}
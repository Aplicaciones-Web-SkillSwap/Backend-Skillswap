using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Domain.Model.Queries;

namespace SkillSwap.Platform.Workspace.Application.QueryServices;

/// <summary>
///     Message query service interface
/// </summary>
public interface IMessageQueryService
{
    /// <summary>
    ///     Handle get messages by session id query
    /// </summary>
    /// <param name="query">The <see cref="GetMessagesBySessionIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Message" /> objects
    /// </returns>
    Task<IEnumerable<Message>> Handle(GetMessagesBySessionIdQuery query, CancellationToken cancellationToken);
}
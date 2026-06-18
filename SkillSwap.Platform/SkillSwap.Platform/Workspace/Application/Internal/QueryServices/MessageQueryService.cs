using SkillSwap.Platform.Workspace.Application.QueryServices;
using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Domain.Model.Queries;
using SkillSwap.Platform.Workspace.Domain.Repositories;

namespace SkillSwap.Platform.Workspace.Application.Internal.QueryServices;

/// <summary>
///     Message query service
/// </summary>
/// <param name="messageRepository">
///     Message repository
/// </param>
public class MessageQueryService(IMessageRepository messageRepository) : IMessageQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Message>> Handle(GetMessagesBySessionIdQuery query, CancellationToken cancellationToken)
    {
        return await messageRepository.FindBySessionIdAsync(query.SessionId, cancellationToken);
    }
}
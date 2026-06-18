using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Domain.Repositories;

namespace SkillSwap.Platform.Workspace.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Message repository implementation
/// </summary>
/// <param name="context">
///     The database context
/// </param>
public class MessageRepository(AppDbContext context)
    : BaseRepository<Message>(context), IMessageRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Message>> FindBySessionIdAsync(int sessionId, CancellationToken cancellationToken)
    {
        return await Context.Set<Message>()
            .Where(m => m.SessionId == sessionId)
            .ToListAsync(cancellationToken);
    }
}
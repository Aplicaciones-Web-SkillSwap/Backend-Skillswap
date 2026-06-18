using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Domain.Repositories;

namespace SkillSwap.Platform.Workspace.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Session repository implementation
/// </summary>
/// <param name="context">
///     The database context
/// </param>
public class SessionRepository(AppDbContext context)
    : BaseRepository<Session>(context), ISessionRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Session>> FindByLearnerIdAsync(int learnerId, CancellationToken cancellationToken)
    {
        return await Context.Set<Session>()
            .Where(s => s.SessionLearnerId.UserId == learnerId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Session>> FindByTutorIdAsync(int tutorId, CancellationToken cancellationToken)
    {
        return await Context.Set<Session>()
            .Where(s => s.SessionTutorId.UserId == tutorId)
            .ToListAsync(cancellationToken);
    }
}
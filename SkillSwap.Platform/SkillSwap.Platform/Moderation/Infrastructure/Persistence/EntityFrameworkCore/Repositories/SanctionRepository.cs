using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SkillSwap.Platform.Moderation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Sanction repository implementation
/// </summary>
/// <param name="context">
///     The database context
/// </param>
public class SanctionRepository(AppDbContext context)
    : BaseRepository<Sanction>(context), ISanctionRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Sanction>> FindByReportIdAsync(int reportId, CancellationToken cancellationToken)
    {
        return await Context.Set<Sanction>()
            .Where(s => s.ReportId == reportId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Sanction>> FindBySanctionedUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Sanction>()
            .Where(s => s.SanctionedUserId.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}

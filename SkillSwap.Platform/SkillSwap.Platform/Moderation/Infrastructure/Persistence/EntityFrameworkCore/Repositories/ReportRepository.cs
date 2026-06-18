using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SkillSwap.Platform.Moderation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Report repository implementation
/// </summary>
/// <param name="context">
///     The database context
/// </param>
public class ReportRepository(AppDbContext context)
    : BaseRepository<Report>(context), IReportRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Report>> FindByReportedUserIdAsync(int reportedUserId,
        CancellationToken cancellationToken)
    {
        return await Context.Set<Report>()
            .Where(r => r.ReportedUserId.UserId == reportedUserId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsPendingReportAsync(int reporterUserId, int reportedUserId,
        CancellationToken cancellationToken)
    {
        return await Context.Set<Report>().AnyAsync(
            r => r.ReporterUserId.UserId == reporterUserId &&
                 r.ReportedUserId.UserId == reportedUserId &&
                 r.Status == "pending",
            cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Reputation.Domain.Model.Aggregates;
using SkillSwap.Platform.Reputation.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SkillSwap.Platform.Reputation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Review repository implementation
/// </summary>
/// <param name="context">
///     The database context
/// </param>
public class ReviewRepository(AppDbContext context)
    : BaseRepository<Review>(context), IReviewRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Review>> FindByTutorIdAsync(int tutorId, CancellationToken cancellationToken)
    {
        return await Context.Set<Review>()
            .Where(r => r.ReviewedTutorId.TutorId == tutorId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByReviewerAndSessionAsync(int reviewerId, int sessionId,
        CancellationToken cancellationToken)
    {
        return await Context.Set<Review>().AnyAsync(
            r => r.ReviewerUserId.UserId == reviewerId && r.SessionId == sessionId,
            cancellationToken);
    }
}
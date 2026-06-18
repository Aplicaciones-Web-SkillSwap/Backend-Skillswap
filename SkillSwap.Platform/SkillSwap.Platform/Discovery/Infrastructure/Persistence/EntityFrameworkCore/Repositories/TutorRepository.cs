using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Discovery.Domain.Model.Aggregates;
using SkillSwap.Platform.Discovery.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SkillSwap.Platform.Discovery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Tutor repository implementation
/// </summary>
/// <param name="context">
///     The database context
/// </param>
public class TutorRepository(AppDbContext context)
    : BaseRepository<Tutor>(context), ITutorRepository
{
    /// <inheritdoc />
    public async Task<Tutor?> FindByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Tutor>()
            .FirstOrDefaultAsync(t => t.TutorUserId.UserId == userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Tutor>> FindBySkillAsync(string skill, CancellationToken cancellationToken)
    {
        return await Context.Set<Tutor>()
            .Where(t => t.TutorSkills.Skills.Contains(skill))
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Tutor>()
            .AnyAsync(t => t.TutorUserId.UserId == userId, cancellationToken);
    }
}
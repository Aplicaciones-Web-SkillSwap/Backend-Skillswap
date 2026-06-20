using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SkillSwap.Platform.Learning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Quiz repository implementation
/// </summary>
public class QuizRepository(AppDbContext context) 
    : BaseRepository<Quiz>(context), IQuizRepository
{
    
    /// <inheritdoc />
    public async Task<IEnumerable<Quiz>> FindByCourseAsync(string course, CancellationToken cancellationToken)
    {
        return await Context.Set<Quiz>()
            .Where(q => q.Course.Contains(course))
            .ToListAsync(cancellationToken);
    }
}
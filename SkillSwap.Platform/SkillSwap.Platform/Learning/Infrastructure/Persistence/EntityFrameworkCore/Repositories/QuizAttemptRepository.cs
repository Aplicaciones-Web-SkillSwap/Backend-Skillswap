using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SkillSwap.Platform.Learning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     QuizAttempt repository implementation
/// </summary>
/// <param name="context">
///     The database context
/// </param>
public class QuizAttemptRepository(AppDbContext context)
    : BaseRepository<QuizAttempt>(context), IQuizAttemptRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<QuizAttempt>> FindByLearnerIdAsync(int learnerId, CancellationToken cancellationToken)
    {
        return await Context.Set<QuizAttempt>()
            .Where(a => a.AttemptLearnerId.UserId == learnerId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuizAttempt>> FindByQuizIdAsync(int quizId, CancellationToken cancellationToken)
    {
        return await Context.Set<QuizAttempt>()
            .Where(a => a.QuizId == quizId)
            .ToListAsync(cancellationToken);
    }
}
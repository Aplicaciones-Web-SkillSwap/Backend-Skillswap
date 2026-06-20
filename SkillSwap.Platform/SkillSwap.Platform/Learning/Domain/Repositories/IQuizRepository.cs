using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Learning.Domain.Repositories;

/// <summary>
///     Quiz repository interface
/// </summary>
public interface IQuizRepository : IBaseRepository<Quiz>
{
    /// <summary>
    ///     Find a quiz by tutor id
    /// </summary>
    Task<IEnumerable<Quiz>> FindByTutorIdAsync(int tutorId, CancellationToken cancellationToken);

    /// <summary>
    ///     Find all quizzes related to a specific course name
    /// </summary>
    Task<IEnumerable<Quiz>> FindByCourseAsync(string course, CancellationToken cancellationToken);
}
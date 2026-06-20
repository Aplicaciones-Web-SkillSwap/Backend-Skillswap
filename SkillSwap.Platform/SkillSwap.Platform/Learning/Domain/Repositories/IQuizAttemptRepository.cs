using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Learning.Domain.Repositories;

/// <summary>
///     QuizAttempt repository interface
/// </summary>
public interface IQuizAttemptRepository : IBaseRepository<QuizAttempt>
{
    /// <summary>
    ///     Find all attempts made by a specific learner
    /// </summary>
    /// <param name="learnerId">
    ///     The unique identifier of the learner
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="QuizAttempt" /> objects
    /// </returns>
    Task<IEnumerable<QuizAttempt>> FindByLearnerIdAsync(int learnerId, CancellationToken cancellationToken);

    /// <summary>
    ///     Find all attempts made for a specific quiz
    /// </summary>
    /// <param name="quizId">
    ///     The unique identifier of the quiz
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="QuizAttempt" /> objects
    /// </returns>
    Task<IEnumerable<QuizAttempt>> FindByQuizIdAsync(int quizId, CancellationToken cancellationToken);
}
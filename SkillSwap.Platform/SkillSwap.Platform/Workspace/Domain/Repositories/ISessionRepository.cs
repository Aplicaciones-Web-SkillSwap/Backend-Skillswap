using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Workspace.Domain.Repositories;

/// <summary>
///     Session repository interface
/// </summary>
public interface ISessionRepository : IBaseRepository<Session>
{
    /// <summary>
    ///     Find all sessions by learner id
    /// </summary>
    /// <param name="learnerId">
    ///     The unique identifier of the learner
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Session" /> objects
    /// </returns>
    Task<IEnumerable<Session>> FindByLearnerIdAsync(int learnerId, CancellationToken cancellationToken);

    /// <summary>
    ///     Find all sessions by tutor id
    /// </summary>
    /// <param name="tutorId">
    ///     The unique identifier of the tutor
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Session" /> objects
    /// </returns>
    Task<IEnumerable<Session>> FindByTutorIdAsync(int tutorId, CancellationToken cancellationToken);
}
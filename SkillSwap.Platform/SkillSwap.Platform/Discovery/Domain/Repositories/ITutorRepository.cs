using SkillSwap.Platform.Discovery.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Discovery.Domain.Repositories;

/// <summary>
///     Tutor repository interface
/// </summary>
public interface ITutorRepository : IBaseRepository<Tutor>
{
    /// <summary>
    ///     Find a tutor by user id
    /// </summary>
    /// <param name="userId">
    ///     The unique identifier of the user
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Tutor" /> if found; otherwise null
    /// </returns>
    Task<Tutor?> FindByUserIdAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Find all tutors that have a specific skill
    /// </summary>
    /// <param name="skill">
    ///     The skill to filter by
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Tutor" /> objects
    /// </returns>
    Task<IEnumerable<Tutor>> FindBySkillAsync(string skill, CancellationToken cancellationToken);

    /// <summary>
    ///     Checks whether a tutor already exists for a given user id
    /// </summary>
    /// <param name="userId">
    ///     The unique identifier of the user
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     True if a tutor already exists; otherwise false
    /// </returns>
    Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken);
}
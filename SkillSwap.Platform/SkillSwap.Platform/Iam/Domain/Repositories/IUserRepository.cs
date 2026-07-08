using SkillSwap.Platform.Iam.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Iam.Domain.Repositories;

/// <summary>
///     User repository interface
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    ///     Find a user by username
    /// </summary>
    /// <param name="username">The username to search for</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="User" /> if found; otherwise null</returns>
    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    ///     Checks whether a user already exists with the given username
    /// </summary>
    /// <param name="username">The username to search for</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if a user already exists; otherwise false</returns>
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    ///     Checks whether a user already exists with the given email
    /// </summary>
    /// <param name="email">The email to search for</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if a user already exists; otherwise false</returns>
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
}

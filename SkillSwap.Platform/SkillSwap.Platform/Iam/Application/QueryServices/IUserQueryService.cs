using SkillSwap.Platform.Iam.Domain.Model.Aggregates;
using SkillSwap.Platform.Iam.Domain.Model.Queries;

namespace SkillSwap.Platform.Iam.Application.QueryServices;

/// <summary>
///     User query service interface
/// </summary>
public interface IUserQueryService
{
    /// <summary>
    ///     Handle get all users query
    /// </summary>
    /// <param name="query">The <see cref="GetAllUsersQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="User" /> objects</returns>
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get user by id query
    /// </summary>
    /// <param name="query">The <see cref="GetUserByIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="User" /> if found; otherwise null</returns>
    Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get user by username query
    /// </summary>
    /// <param name="query">The <see cref="GetUserByUsernameQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="User" /> if found; otherwise null</returns>
    Task<User?> Handle(GetUserByUsernameQuery query, CancellationToken cancellationToken);
}

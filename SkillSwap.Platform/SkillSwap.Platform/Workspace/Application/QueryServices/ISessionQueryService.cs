using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Domain.Model.Queries;

namespace SkillSwap.Platform.Workspace.Application.QueryServices;

/// <summary>
///     Session query service interface
/// </summary>
public interface ISessionQueryService
{
    /// <summary>
    ///     Handle get all sessions query
    /// </summary>
    /// <param name="query">The <see cref="GetAllSessionsQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Session" /> objects
    /// </returns>
    Task<IEnumerable<Session>> Handle(GetAllSessionsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get session by id query
    /// </summary>
    /// <param name="query">The <see cref="GetSessionByIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Session" /> if found; otherwise null
    /// </returns>
    Task<Session?> Handle(GetSessionByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get sessions by learner id query
    /// </summary>
    /// <param name="query">The <see cref="GetSessionsByLearnerIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Session" /> objects
    /// </returns>
    Task<IEnumerable<Session>> Handle(GetSessionsByLearnerIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get sessions by tutor id query
    /// </summary>
    /// <param name="query">The <see cref="GetSessionsByTutorIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Session" /> objects
    /// </returns>
    Task<IEnumerable<Session>> Handle(GetSessionsByTutorIdQuery query, CancellationToken cancellationToken);
}
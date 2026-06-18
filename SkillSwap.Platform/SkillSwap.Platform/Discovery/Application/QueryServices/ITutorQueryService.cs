using SkillSwap.Platform.Discovery.Domain.Model.Aggregates;
using SkillSwap.Platform.Discovery.Domain.Model.Queries;

namespace SkillSwap.Platform.Discovery.Application.QueryServices;

/// <summary>
///     Tutor query service interface
/// </summary>
public interface ITutorQueryService
{
    /// <summary>
    ///     Handle get all tutors query
    /// </summary>
    /// <param name="query">The <see cref="GetAllTutorsQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Tutor" /> objects
    /// </returns>
    Task<IEnumerable<Tutor>> Handle(GetAllTutorsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get tutor by id query
    /// </summary>
    /// <param name="query">The <see cref="GetTutorByIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Tutor" /> if found; otherwise null
    /// </returns>
    Task<Tutor?> Handle(GetTutorByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get tutor by user id query
    /// </summary>
    /// <param name="query">The <see cref="GetTutorByUserIdQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Tutor" /> if found; otherwise null
    /// </returns>
    Task<Tutor?> Handle(GetTutorByUserIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get tutors by skill query
    /// </summary>
    /// <param name="query">The <see cref="GetTutorsBySkillQuery" /> query</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A list of <see cref="Tutor" /> objects
    /// </returns>
    Task<IEnumerable<Tutor>> Handle(GetTutorsBySkillQuery query, CancellationToken cancellationToken);
}
using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Model.Queries;

namespace SkillSwap.Platform.Moderation.Application.QueryServices;

/// <summary>
///     Report query service interface
/// </summary>
public interface IReportQueryService
{
    /// <summary>
    ///     Handle get all reports
    /// </summary>
    Task<IEnumerable<Report>> Handle(GetAllReportsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get report by id
    /// </summary>
    Task<Report?> Handle(GetReportByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get reports by reported user
    /// </summary>
    Task<IEnumerable<Report>> Handle(GetReportsByReportedUserQuery query, CancellationToken cancellationToken);
}

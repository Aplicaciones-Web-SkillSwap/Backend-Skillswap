using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Model.Queries;

namespace SkillSwap.Platform.Moderation.Application.QueryServices;

/// <summary>
///     Sanction query service interface
/// </summary>
public interface ISanctionQueryService
{
    /// <summary>
    ///     Handle get all sanctions
    /// </summary>
    Task<IEnumerable<Sanction>> Handle(GetAllSanctionsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get sanction by id
    /// </summary>
    Task<Sanction?> Handle(GetSanctionByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get sanctions by report id
    /// </summary>
    Task<IEnumerable<Sanction>> Handle(GetSanctionsByReportIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get sanctions by sanctioned user id
    /// </summary>
    Task<IEnumerable<Sanction>> Handle(GetSanctionsBySanctionedUserIdQuery query, CancellationToken cancellationToken);
}

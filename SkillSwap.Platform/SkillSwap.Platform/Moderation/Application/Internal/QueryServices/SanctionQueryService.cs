using SkillSwap.Platform.Moderation.Application.QueryServices;
using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Model.Queries;
using SkillSwap.Platform.Moderation.Domain.Repositories;

namespace SkillSwap.Platform.Moderation.Application.Internal.QueryServices;

/// <summary>
///     Sanction query service
/// </summary>
/// <param name="sanctionRepository">
///     Sanction repository
/// </param>
public class SanctionQueryService(ISanctionRepository sanctionRepository) : ISanctionQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Sanction>> Handle(GetAllSanctionsQuery query, CancellationToken cancellationToken)
    {
        return await sanctionRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Sanction?> Handle(GetSanctionByIdQuery query, CancellationToken cancellationToken)
    {
        return await sanctionRepository.FindByIdAsync(query.SanctionId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Sanction>> Handle(GetSanctionsByReportIdQuery query,
        CancellationToken cancellationToken)
    {
        return await sanctionRepository.FindByReportIdAsync(query.ReportId, cancellationToken);
    }
}

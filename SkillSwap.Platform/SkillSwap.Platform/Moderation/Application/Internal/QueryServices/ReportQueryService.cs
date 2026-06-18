using SkillSwap.Platform.Moderation.Application.QueryServices;
using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Model.Queries;
using SkillSwap.Platform.Moderation.Domain.Repositories;

namespace SkillSwap.Platform.Moderation.Application.Internal.QueryServices;

/// <summary>
///     Report query service
/// </summary>
/// <param name="reportRepository">
///     Report repository
/// </param>
public class ReportQueryService(IReportRepository reportRepository) : IReportQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Report>> Handle(GetAllReportsQuery query, CancellationToken cancellationToken)
    {
        return await reportRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Report?> Handle(GetReportByIdQuery query, CancellationToken cancellationToken)
    {
        return await reportRepository.FindByIdAsync(query.ReportId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Report>> Handle(GetReportsByReportedUserQuery query,
        CancellationToken cancellationToken)
    {
        return await reportRepository.FindByReportedUserIdAsync(query.ReportedUserId, cancellationToken);
    }
}

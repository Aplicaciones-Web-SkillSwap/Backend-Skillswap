namespace SkillSwap.Platform.Moderation.Domain.Model.Queries;

/// <summary>
///     Get sanctions by report id query
/// </summary>
/// <param name="ReportId">
///     The unique identifier of the report.
/// </param>
public record GetSanctionsByReportIdQuery(int ReportId);

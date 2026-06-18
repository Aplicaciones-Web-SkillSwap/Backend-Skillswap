namespace SkillSwap.Platform.Moderation.Domain.Model.Queries;

/// <summary>
///     Get report by id query
/// </summary>
/// <param name="ReportId">
///     The unique identifier of the report.
/// </param>
public record GetReportByIdQuery(int ReportId);

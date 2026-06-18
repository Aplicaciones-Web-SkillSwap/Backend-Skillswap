namespace SkillSwap.Platform.Moderation.Interfaces.Rest.Resources;

/// <summary>
///     Report resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the report
/// </param>
/// <param name="ReporterUserId">
///     The unique identifier of the user who created the report
/// </param>
/// <param name="ReportedUserId">
///     The unique identifier of the reported user
/// </param>
/// <param name="Reason">
///     The reason for the report
/// </param>
/// <param name="Status">
///     The current status of the report (pending / resolved)
/// </param>
/// <param name="Closed">
///     Whether the report has been closed
/// </param>
/// <param name="ReportedAt">
///     The date and time the report was created
/// </param>
public record ReportResource(
    int Id,
    int ReporterUserId,
    int ReportedUserId,
    string Reason,
    string Status,
    bool Closed,
    DateTime ReportedAt);

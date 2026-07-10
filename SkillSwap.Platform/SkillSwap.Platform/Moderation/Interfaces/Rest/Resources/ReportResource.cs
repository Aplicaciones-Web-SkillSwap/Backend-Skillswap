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
/// <param name="SessionId">
///     The unique identifier of the session related to the report
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
/// <param name="ResolvedAt">
///     The date and time the report was resolved (closed), if it has been
/// </param>
public record ReportResource(
    int Id,
    int ReporterUserId,
    int ReportedUserId,
    int SessionId,
    string Reason,
    string Status,
    bool Closed,
    DateTime ReportedAt,
    DateTime? ResolvedAt);
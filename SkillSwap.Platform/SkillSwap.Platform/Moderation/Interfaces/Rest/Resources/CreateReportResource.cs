namespace SkillSwap.Platform.Moderation.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new report
/// </summary>
/// <param name="ReporterUserId">
///     The unique identifier of the user who creates the report
/// </param>
/// <param name="ReportedUserId">
///     The unique identifier of the user being reported
/// </param>
/// <param name="SessionId">
///     The unique identifier of the session related to the report
/// </param>
/// <param name="Reason">
///     The reason for the report
/// </param>
public record CreateReportResource(
    int ReporterUserId,
    int ReportedUserId,
    int SessionId,
    string Reason);
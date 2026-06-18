namespace SkillSwap.Platform.Moderation.Domain.Model.Commands;

/// <summary>
///     Close Report Command
/// </summary>
/// <param name="ReportId">
///     The unique identifier of the report to close.
/// </param>
public record CloseReportCommand(int ReportId);

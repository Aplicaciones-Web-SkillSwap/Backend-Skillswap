using SkillSwap.Platform.Moderation.Domain.Model.Commands;
using SkillSwap.Platform.Moderation.Domain.Model.ValueObjects;

namespace SkillSwap.Platform.Moderation.Domain.Model.Aggregates;

/// <summary>
///     Report Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Report aggregate root.
///     It contains the properties and methods to manage a user report.
/// </remarks>
public partial class Report
{
    public Report()
    {
        ReporterUserId = new ReporterId();
        ReportedUserId = new ReportedUserId();
        ReportSessionId = new SessionId();
        Reason = string.Empty;
        Status = "pending";
        Closed = false;
        ReportedAt = DateTime.UtcNow;
    }

    public Report(CreateReportCommand command)
    {
        ReporterUserId = new ReporterId(command.ReporterUserId);
        ReportedUserId = new ReportedUserId(command.ReportedUserId);
        ReportSessionId = new SessionId(command.SessionId);
        Reason = command.Reason;
        Status = "pending";
        Closed = false;
        ReportedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public ReporterId ReporterUserId { get; private set; }
    public ReportedUserId ReportedUserId { get; private set; }
    public SessionId ReportSessionId { get; private set; }
    public string Reason { get; private set; }
    public string Status { get; private set; }
    public bool Closed { get; private set; }
    public DateTime ReportedAt { get; }
    public DateTime? ResolvedAt { get; private set; }

    public bool IsPending => Status == "pending";

    public void Close()
    {
        Closed = true;
        Status = "resolved";
        ResolvedAt = DateTime.UtcNow;
    }
}
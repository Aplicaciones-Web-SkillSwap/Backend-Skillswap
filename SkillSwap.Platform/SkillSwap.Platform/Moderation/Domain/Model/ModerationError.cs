namespace SkillSwap.Platform.Moderation.Domain.Model;

public enum ModerationError
{
    None,
    ReportNotFound,
    SanctionNotFound,
    SelfReportNotAllowed,
    DuplicatePendingReport,
    InvalidSanctionDuration,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}

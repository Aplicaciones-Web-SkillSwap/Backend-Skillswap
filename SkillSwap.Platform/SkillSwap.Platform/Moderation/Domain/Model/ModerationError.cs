namespace SkillSwap.Platform.Moderation.Domain.Model;

public enum ModerationError
{
    None,
    ReportNotFound,
    SanctionNotFound,
    SelfReportNotAllowed,
    DuplicatePendingReport,
    SessionNotFound,
    ReporterNotSessionParticipant,
    ReportedUserNotSessionParticipant,
    InvalidSanctionDuration,
    NotSanctionOwner,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}

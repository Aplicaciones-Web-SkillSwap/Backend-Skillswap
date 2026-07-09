namespace SkillSwap.Platform.Workspace.Domain.Model;

public enum WorkspaceError
{
    None,
    SessionNotFound,
    MessageNotFound,
    InvalidSessionStatus,
    SelfSessionNotAllowed,
    NotSessionParticipant,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
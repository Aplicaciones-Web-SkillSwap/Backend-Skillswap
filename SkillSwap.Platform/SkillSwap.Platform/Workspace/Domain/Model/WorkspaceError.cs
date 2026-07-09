namespace SkillSwap.Platform.Workspace.Domain.Model;

public enum WorkspaceError
{
    None,
    SessionNotFound,
    MessageNotFound,
    InvalidSessionStatus,
    SelfSessionNotAllowed,
    PendingSessionAlreadyExists,
    NotSessionParticipant,
    NotYourTurn,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
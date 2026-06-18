namespace SkillSwap.Platform.Workspace.Domain.Model;

public enum WorkspaceError
{
    None,
    SessionNotFound,
    MessageNotFound,
    InvalidSessionStatus,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
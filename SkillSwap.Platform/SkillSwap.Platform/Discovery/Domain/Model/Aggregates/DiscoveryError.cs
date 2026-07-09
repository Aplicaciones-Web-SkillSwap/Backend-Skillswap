namespace SkillSwap.Platform.Discovery.Domain.Model;

public enum DiscoveryError
{
    None,
    TutorNotFound,
    TutorAlreadyExists,
    NotTutorOwner,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
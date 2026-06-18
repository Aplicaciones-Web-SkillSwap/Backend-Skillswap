namespace SkillSwap.Platform.Discovery.Domain.Model;

public enum DiscoveryError
{
    None,
    TutorNotFound,
    TutorAlreadyExists,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
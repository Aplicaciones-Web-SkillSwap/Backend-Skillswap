namespace SkillSwap.Platform.Iam.Domain.Model;

public enum IamError
{
    None,
    InvalidCredentials,
    UsernameAlreadyTaken,
    EmailAlreadyTaken,
    InvalidInstitutionalEmail,
    UserNotFound,
    NotProfileOwner,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}

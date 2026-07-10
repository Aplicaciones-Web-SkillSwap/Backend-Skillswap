namespace SkillSwap.Platform.Iam.Domain.Model;

public enum IamError
{
    None,
    InvalidCredentials,
    UserBanned,
    UsernameAlreadyTaken,
    EmailAlreadyTaken,
    InvalidInstitutionalEmail,
    UserNotFound,
    NotProfileOwner,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}

namespace SkillSwap.Platform.Payments.Domain.Model;

public enum PaymentsError
{
    None,
    WalletNotFound,
    WalletAlreadyExists,
    InsufficientFunds,
    InvalidAmount,
    SenderWalletNotFound,
    ReceiverWalletNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
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
    SelfDonationNotAllowed,
    NoCompletedSessionWithTutor,
    InvalidPaymentMethodType,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
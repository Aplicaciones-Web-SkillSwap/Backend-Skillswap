namespace SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

/// <summary>
///     Donation result resource for REST API
/// </summary>
/// <param name="TransactionId">
///     The unique identifier of the created transaction
/// </param>
/// <param name="AmountSent">
///     The total amount sent by the student
/// </param>
/// <param name="PlatformFee">
///     The platform fee deducted (5% of the amount)
/// </param>
/// <param name="AmountReceived">
///     The net amount received by the tutor (95% of the amount)
/// </param>
/// <param name="NewSenderBalance">
///     The sender's wallet balance after the donation
/// </param>
/// <param name="NewReceiverBalance">
///     The receiver's wallet balance after the donation
/// </param>
public record DonationResultResource(
    int TransactionId,
    decimal AmountSent,
    decimal PlatformFee,
    decimal AmountReceived,
    decimal NewSenderBalance,
    decimal NewReceiverBalance);
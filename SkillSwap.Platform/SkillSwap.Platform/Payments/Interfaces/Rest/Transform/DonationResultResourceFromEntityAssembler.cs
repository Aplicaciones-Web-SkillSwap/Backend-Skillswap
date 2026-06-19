using SkillSwap.Platform.Payments.Domain.Model;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="DonationResult" /> into a
///     <see cref="DonationResultResource" />.
/// </summary>
public static class DonationResultResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="DonationResult" /> to its <see cref="DonationResultResource" /> representation.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="DonationResult" /> to convert.
    /// </param>
    /// <returns>
    ///     A <see cref="DonationResultResource" /> object representing the donation outcome.
    /// </returns>
    public static DonationResultResource ToResourceFromEntity(DonationResult result)
    {
        return new DonationResultResource(
            result.TransactionId,
            result.AmountSent,
            result.PlatformFee,
            result.AmountReceived,
            result.NewSenderBalance,
            result.NewReceiverBalance);
    }
}
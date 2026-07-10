using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Transaction" /> aggregate into a
///     <see cref="TransactionResource" />.
/// </summary>
public static class TransactionResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Transaction" /> aggregate to its <see cref="TransactionResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Transaction" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A <see cref="TransactionResource" /> object representing the provided transaction.
    /// </returns>
    public static TransactionResource ToResourceFromEntity(Transaction entity)
    {
        return new TransactionResource(
            entity.Id,
            entity.WalletId,
            entity.AmountSent,
            entity.PlatformFee,
            entity.Amount,
            entity.Type,
            entity.Description,
            entity.CreatedAt);
    }
}
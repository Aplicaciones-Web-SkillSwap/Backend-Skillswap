using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Wallet" /> aggregate into a <see cref="WalletResource" />.
/// </summary>
public static class WalletResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="Wallet" /> aggregate to its <see cref="WalletResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Wallet" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A <see cref="WalletResource" /> object representing the provided wallet.
    /// </returns>
    public static WalletResource ToResourceFromEntity(Wallet entity)
    {
        return new WalletResource(
            entity.Id,
            entity.WalletOwnerId.UserId,
            entity.Balance);
    }
}
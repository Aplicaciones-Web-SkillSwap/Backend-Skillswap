using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="AddFundsResource" /> into a
///     <see cref="AddFundsCommand" />.
/// </summary>
public static class AddFundsCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="AddFundsResource" /> to a <see cref="AddFundsCommand" />.
    /// </summary>
    /// <param name="walletId">
    ///     The unique identifier of the wallet to add funds to.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="AddFundsResource" /> containing the amount to add.
    /// </param>
    /// <returns>
    ///     A new <see cref="AddFundsCommand" /> instance.
    /// </returns>
    public static AddFundsCommand ToCommandFromResource(int walletId, AddFundsResource resource)
    {
        return new AddFundsCommand(walletId, resource.Amount);
    }
}
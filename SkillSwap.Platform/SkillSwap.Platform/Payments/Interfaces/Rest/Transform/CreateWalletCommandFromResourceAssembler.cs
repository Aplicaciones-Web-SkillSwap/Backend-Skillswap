using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateWalletResource" /> into a
///     <see cref="CreateWalletCommand" />.
/// </summary>
public static class CreateWalletCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateWalletResource" /> to a <see cref="CreateWalletCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateWalletResource" /> containing the data for creating a wallet.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateWalletCommand" /> instance.
    /// </returns>
    public static CreateWalletCommand ToCommandFromResource(CreateWalletResource resource)
    {
        return new CreateWalletCommand(resource.UserId);
    }
}
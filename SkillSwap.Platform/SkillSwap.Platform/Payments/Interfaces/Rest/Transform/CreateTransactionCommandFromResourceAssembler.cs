using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateTransactionResource" /> into a
///     <see cref="CreateTransactionCommand" />.
/// </summary>
public static class CreateTransactionCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateTransactionResource" /> to a <see cref="CreateTransactionCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateTransactionResource" /> containing the data for creating a transaction.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateTransactionCommand" /> instance.
    /// </returns>
    public static CreateTransactionCommand ToCommandFromResource(CreateTransactionResource resource)
    {
        return new CreateTransactionCommand(
            resource.WalletId,
            resource.Amount,
            resource.Type,
            resource.Description,
            resource.Amount,
            0m);
    }
}
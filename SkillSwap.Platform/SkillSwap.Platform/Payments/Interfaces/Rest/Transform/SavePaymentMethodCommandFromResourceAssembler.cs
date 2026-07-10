using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="SavePaymentMethodResource" /> into a
///     <see cref="SavePaymentMethodCommand" />.
/// </summary>
public static class SavePaymentMethodCommandFromResourceAssembler
{
    /// <param name="resource">
    ///     The <see cref="SavePaymentMethodResource" /> containing the data to save.
    /// </param>
    /// <param name="userId">
    ///     The authenticated caller's user id, used as the owner instead of any value the client
    ///     might submit in the resource body.
    /// </param>
    public static SavePaymentMethodCommand ToCommandFromResource(SavePaymentMethodResource resource, int userId)
    {
        return new SavePaymentMethodCommand(userId, resource.Type, resource.DisplayLabel);
    }
}

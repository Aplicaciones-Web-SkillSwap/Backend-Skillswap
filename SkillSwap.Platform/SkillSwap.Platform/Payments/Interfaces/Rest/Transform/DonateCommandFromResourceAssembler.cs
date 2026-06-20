using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="DonateResource" /> into a <see cref="DonateCommand" />.
/// </summary>
public static class DonateCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="DonateResource" /> to a <see cref="DonateCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="DonateResource" /> containing the donation data.
    /// </param>
    /// <returns>
    ///     A new <see cref="DonateCommand" /> instance.
    /// </returns>
    public static DonateCommand ToCommandFromResource(DonateResource resource)
    {
        return new DonateCommand(
            resource.FromUserId,
            resource.ToUserId,
            resource.Amount,
            resource.Description);
    }
}
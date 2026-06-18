using SkillSwap.Platform.Moderation.Domain.Model.Commands;
using SkillSwap.Platform.Moderation.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Moderation.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateSanctionResource" /> into a
///     <see cref="CreateSanctionCommand" />.
/// </summary>
public static class CreateSanctionCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateSanctionResource" /> to a <see cref="CreateSanctionCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateSanctionResource" /> containing the data for creating a sanction. Must not be null.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateSanctionCommand" /> instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="resource" /> is null.</exception>
    public static CreateSanctionCommand ToCommandFromResource(CreateSanctionResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateSanctionResource cannot be null when converting to command.");
        return new CreateSanctionCommand(
            resource.ReportId,
            resource.SanctionedUserId,
            resource.Type,
            resource.Description,
            resource.DurationDays
        );
    }
}

using SkillSwap.Platform.Moderation.Domain.Model.Commands;
using SkillSwap.Platform.Moderation.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Moderation.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreateReportResource" /> into a
///     <see cref="CreateReportCommand" />.
/// </summary>
public static class CreateReportCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateReportResource" /> to a <see cref="CreateReportCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateReportResource" /> containing the data for creating a report. Must not be null.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateReportCommand" /> instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="resource" /> is null.</exception>
    public static CreateReportCommand ToCommandFromResource(CreateReportResource resource, int reporterUserId)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateReportResource cannot be null when converting to command.");
        return new CreateReportCommand(
            reporterUserId,
            resource.ReportedUserId,
            resource.SessionId,
            resource.Reason
        );
    }
}
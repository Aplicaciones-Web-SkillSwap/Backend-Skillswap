namespace SkillSwap.Platform.Moderation.Interfaces.Rest.Resources;

/// <summary>
///     Sanction resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the sanction
/// </param>
/// <param name="ReportId">
///     The unique identifier of the related report
/// </param>
/// <param name="SanctionedUserId">
///     The unique identifier of the sanctioned user
/// </param>
/// <param name="Type">
///     The type of sanction (e.g. warning, ban)
/// </param>
/// <param name="Description">
///     The description of the sanction
/// </param>
/// <param name="DurationDays">
///     The number of days the sanction lasts
/// </param>
/// <param name="IsPermanent">
///     Whether this sanction (a ban) never expires
/// </param>
/// <param name="CreatedAt">
///     The date and time the sanction was created
/// </param>
/// <param name="AcknowledgedAt">
///     The date and time the sanctioned user acknowledged this sanction, if they have
/// </param>
public record SanctionResource(
    int Id,
    int ReportId,
    int SanctionedUserId,
    string Type,
    string Description,
    int DurationDays,
    bool IsPermanent,
    DateTime CreatedAt,
    DateTime? AcknowledgedAt);

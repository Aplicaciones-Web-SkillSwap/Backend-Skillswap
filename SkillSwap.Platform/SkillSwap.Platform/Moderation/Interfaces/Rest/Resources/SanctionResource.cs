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
public record SanctionResource(
    int Id,
    int ReportId,
    int SanctionedUserId,
    string Type,
    string Description,
    int DurationDays);

namespace SkillSwap.Platform.Moderation.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new sanction
/// </summary>
/// <param name="ReportId">
///     The unique identifier of the report that originated this sanction
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
public record CreateSanctionResource(
    int ReportId,
    int SanctionedUserId,
    string Type,
    string Description,
    int DurationDays);

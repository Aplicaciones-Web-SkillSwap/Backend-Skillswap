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
///     The number of days the sanction lasts. Ignored when <paramref name="IsPermanent" /> is true
/// </param>
/// <param name="IsPermanent">
///     Whether this sanction (a ban) never expires
/// </param>
public record CreateSanctionResource(
    int ReportId,
    int SanctionedUserId,
    string Type,
    string Description,
    int DurationDays,
    bool IsPermanent = false);

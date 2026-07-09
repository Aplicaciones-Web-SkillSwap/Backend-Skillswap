namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

/// <summary>
///     Resource for proposing a new date/time for a pending session
/// </summary>
/// <param name="NewScheduledAt">
///     The newly proposed date and time for the session
/// </param>
public record RescheduleSessionResource(DateTime NewScheduledAt);

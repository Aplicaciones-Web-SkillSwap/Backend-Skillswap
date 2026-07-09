namespace SkillSwap.Platform.Workspace.Domain.Model.Commands;

/// <summary>
///     Reschedule Session Command
/// </summary>
/// <param name="SessionId">
///     The unique identifier of the session to reschedule
/// </param>
/// <param name="NewScheduledAt">
///     The newly proposed date and time for the session
/// </param>
/// <param name="ActorUserId">
///     The identifier of the authenticated user proposing the new date, derived from the JWT
/// </param>
public record RescheduleSessionCommand(int SessionId, DateTime NewScheduledAt, int ActorUserId);

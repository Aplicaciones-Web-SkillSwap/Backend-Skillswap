namespace SkillSwap.Platform.Moderation.Domain.Model.Commands;

/// <summary>
///     Acknowledge Sanction Command
/// </summary>
/// <param name="SanctionId">
///     The unique identifier of the sanction being acknowledged.
/// </param>
/// <param name="ActorUserId">
///     The unique identifier of the user acknowledging the sanction. Must be the sanctioned user.
/// </param>
public record AcknowledgeSanctionCommand(int SanctionId, int ActorUserId);

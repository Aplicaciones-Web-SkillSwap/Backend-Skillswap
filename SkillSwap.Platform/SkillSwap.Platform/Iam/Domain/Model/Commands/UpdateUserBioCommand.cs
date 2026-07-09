namespace SkillSwap.Platform.Iam.Domain.Model.Commands;

/// <summary>
///     Update User Bio Command
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user whose bio is being updated
/// </param>
/// <param name="Bio">
///     The new bio text
/// </param>
/// <param name="ActorUserId">
///     The identifier of the authenticated user performing the update, derived from the JWT
/// </param>
public record UpdateUserBioCommand(int UserId, string Bio, int ActorUserId);

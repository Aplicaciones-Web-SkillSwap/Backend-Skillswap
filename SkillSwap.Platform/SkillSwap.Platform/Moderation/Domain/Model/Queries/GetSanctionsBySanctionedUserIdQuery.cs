namespace SkillSwap.Platform.Moderation.Domain.Model.Queries;

/// <summary>
///     Get sanctions by sanctioned user id query
/// </summary>
/// <param name="SanctionedUserId">
///     The unique identifier of the sanctioned user.
/// </param>
public record GetSanctionsBySanctionedUserIdQuery(int SanctionedUserId);

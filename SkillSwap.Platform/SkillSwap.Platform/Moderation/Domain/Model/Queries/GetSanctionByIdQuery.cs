namespace SkillSwap.Platform.Moderation.Domain.Model.Queries;

/// <summary>
///     Get sanction by id query
/// </summary>
/// <param name="SanctionId">
///     The unique identifier of the sanction.
/// </param>
public record GetSanctionByIdQuery(int SanctionId);

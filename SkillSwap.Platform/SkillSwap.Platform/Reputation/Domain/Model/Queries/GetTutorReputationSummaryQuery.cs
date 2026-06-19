namespace SkillSwap.Platform.Reputation.Domain.Model.Queries;

/// <summary>
///     Get tutor reputation summary query
/// </summary>
/// <param name="TutorId">
///     The unique identifier of the tutor
/// </param>
public record GetTutorReputationSummaryQuery(int TutorId);
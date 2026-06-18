namespace SkillSwap.Platform.Workspace.Domain.Model.Queries;

/// <summary>
///     Get sessions by learner id query
/// </summary>
/// <param name="LearnerId">
///     The unique identifier of the learner
/// </param>
public record GetSessionsByLearnerIdQuery(int LearnerId);
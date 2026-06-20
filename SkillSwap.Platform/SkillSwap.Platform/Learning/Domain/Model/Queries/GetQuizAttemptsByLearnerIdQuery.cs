namespace SkillSwap.Platform.Learning.Domain.Model.Queries;

/// <summary>
///     Get quiz attempts by learner id query
/// </summary>
/// <param name="LearnerId">
///     The unique identifier of the learner
/// </param>
public record GetQuizAttemptsByLearnerIdQuery(int LearnerId);
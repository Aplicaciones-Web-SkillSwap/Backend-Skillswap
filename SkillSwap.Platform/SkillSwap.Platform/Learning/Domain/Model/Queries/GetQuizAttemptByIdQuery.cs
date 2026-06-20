namespace SkillSwap.Platform.Learning.Domain.Model.Queries;

/// <summary>
///     Get quiz attempt by id query
/// </summary>
/// <param name="AttemptId">
///     The unique identifier of the quiz attempt
/// </param>
public record GetQuizAttemptByIdQuery(int AttemptId);
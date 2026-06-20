namespace SkillSwap.Platform.Learning.Domain.Model.Queries;

/// <summary>
///     Get quiz attempts by quiz id query
/// </summary>
/// <param name="QuizId">
///     The unique identifier of the quiz
/// </param>
public record GetQuizAttemptsByQuizIdQuery(int QuizId);
namespace SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

/// <summary>
///     QuizAttempt resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the attempt
/// </param>
/// <param name="QuizId">
///     The unique identifier of the quiz that was attempted
/// </param>
/// <param name="LearnerId">
///     The unique identifier of the learner who submitted the attempt
/// </param>
/// <param name="SessionId">
///     The unique identifier of the related tutoring session (0 if none)
/// </param>
/// <param name="SelectedAnswers">
///     The list of selected answer indexes, one per question
/// </param>
/// <param name="Score">
///     The number of correctly answered questions
/// </param>
/// <param name="Total">
///     The total number of questions in the quiz at the time of the attempt
/// </param>
/// <param name="CompletedAt">
///     The date and time the attempt was completed
/// </param>
public record QuizAttemptResource(
    int Id,
    int QuizId,
    int LearnerId,
    int SessionId,
    List<int> SelectedAnswers,
    int Score,
    int Total,
    DateTime CompletedAt);
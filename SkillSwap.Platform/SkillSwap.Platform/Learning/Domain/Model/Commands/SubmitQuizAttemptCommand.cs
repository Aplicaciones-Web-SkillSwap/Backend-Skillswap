namespace SkillSwap.Platform.Learning.Domain.Model.Commands;

/// <summary>
///     Submit Quiz Attempt Command
/// </summary>
/// <param name="QuizId">
///     The unique identifier of the quiz being attempted
/// </param>
/// <param name="LearnerId">
///     The unique identifier of the learner submitting the attempt
/// </param>
/// <param name="SessionId">
///     The unique identifier of the tutoring session this attempt is related to (optional, 0 if none)
/// </param>
/// <param name="SelectedAnswers">
///     The list of selected answer indexes, one per question, in question order
/// </param>
public record SubmitQuizAttemptCommand(
    int QuizId,
    int LearnerId,
    int SessionId,
    List<int> SelectedAnswers);
namespace SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

/// <summary>
///     Resource for submitting a quiz attempt
/// </summary>
/// <param name="LearnerId">
///     The unique identifier of the learner submitting the attempt
/// </param>
/// <param name="SessionId">
///     The unique identifier of the tutoring session this attempt is related to (0 if none)
/// </param>
/// <param name="SelectedAnswers">
///     The list of selected answer indexes, one per question, in question order
/// </param>
public record SubmitQuizAttemptResource(
    int LearnerId,
    int SessionId,
    List<int> SelectedAnswers);
namespace SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

/// <summary>
///     Resource to add a question to a quiz.
/// </summary>
/// <param name="QuestionString">The text of the question.</param>
/// <param name="Answers">The list of possible answers.</param>
/// <param name="CorrectAnswer">The index of the correct answer.</param>
public record AddQuestionResource(
    string QuestionString, 
    string[] Answers, 
    int CorrectAnswer);
namespace SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

public record QuestionResource(
    string QuestionString,
    string[] Answers,
    int CorrectAnswer);
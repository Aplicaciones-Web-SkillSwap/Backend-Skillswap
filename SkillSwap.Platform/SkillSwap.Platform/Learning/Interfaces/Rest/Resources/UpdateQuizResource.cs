namespace SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

public record UpdateQuizInfoResource(
    string Title,
    string Course,
    string Description,
    string status);
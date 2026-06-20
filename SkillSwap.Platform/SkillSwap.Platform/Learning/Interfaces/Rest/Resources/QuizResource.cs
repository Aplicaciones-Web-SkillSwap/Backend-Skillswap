namespace SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

public record QuizResource(
    int Id,
    string Title,
    string Course,
    string Description,
    int TutorId,
    List<QuestionResource> Questions);

namespace SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

public record CreateQuizResource(
    string Title,
    string Course, 
    string Description, 
    int TutorId);
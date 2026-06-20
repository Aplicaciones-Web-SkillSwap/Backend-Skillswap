namespace SkillSwap.Platform.Learning.Domain.Model.Commands;

public record UpdateQuizInfoCommand(
    int QuizId, 
    string Title, 
    string Course, 
    string Description,
    string Status
);
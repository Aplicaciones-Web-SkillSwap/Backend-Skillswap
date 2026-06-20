using SkillSwap.Platform.Learning.Domain.Model.ValueObjects;

namespace SkillSwap.Platform.Learning.Domain.Model.Commands;

public record CreateQuizCommand(
    string Title,
    string Course, 
    string Description, 
    int TutorId
    
);
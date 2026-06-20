namespace SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

public record UpdateQuestionResource(
    string Question,        
    List<string> Answers,    
    int CorrectAnswerIndex     
);
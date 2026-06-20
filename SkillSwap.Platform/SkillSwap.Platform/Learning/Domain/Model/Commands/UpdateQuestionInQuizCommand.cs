namespace SkillSwap.Platform.Learning.Domain.Model.Commands;

public record UpdateQuestionInQuizCommand(
    int QuizId, 
    int Index, 
    string QuestionString, 
    string[] Answers, 
    int CorrectAnswer);
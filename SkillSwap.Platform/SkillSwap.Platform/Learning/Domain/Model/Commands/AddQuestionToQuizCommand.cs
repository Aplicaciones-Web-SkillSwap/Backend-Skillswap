namespace SkillSwap.Platform.Learning.Domain.Model.Commands;

public record AddQuestionToQuizCommand(int QuizId,
    string QuestionString,
    string[] Answers, 
    int CorrectAnswer);
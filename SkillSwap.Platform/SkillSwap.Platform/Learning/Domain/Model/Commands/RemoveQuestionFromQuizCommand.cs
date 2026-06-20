namespace SkillSwap.Platform.Learning.Domain.Model.Commands;

public record RemoveQuestionFromQuizCommand(int QuizId, int Index);
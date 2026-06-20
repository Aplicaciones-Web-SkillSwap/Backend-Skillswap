using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Domain.Model.ValueObjects;

namespace SkillSwap.Platform.Learning.Domain.Model.Aggregates;

/// <summary>
///     QuizAttempt Aggregate Root
/// </summary>
/// <remarks>
///     This class represents a learner's attempt to solve a quiz, storing the answers
///     selected, the resulting score, and the total number of questions at the time
///     of the attempt.
/// </remarks>
public partial class QuizAttempt
{
    public QuizAttempt()
    {
        QuizId = 0;
        AttemptLearnerId = new LearnerId();
        SessionId = 0;
        SelectedAnswers = [];
        Score = 0;
        Total = 0;
        CompletedAt = DateTime.UtcNow;
    }

    public QuizAttempt(SubmitQuizAttemptCommand command, int score, int total)
    {
        QuizId = command.QuizId;
        AttemptLearnerId = new LearnerId(command.LearnerId);
        SessionId = command.SessionId;
        SelectedAnswers = command.SelectedAnswers;
        Score = score;
        Total = total;
        CompletedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public int QuizId { get; private set; }
    public LearnerId AttemptLearnerId { get; private set; }
    public int SessionId { get; private set; }
    public List<int> SelectedAnswers { get; private set; }
    public int Score { get; private set; }
    public int Total { get; private set; }
    public DateTime CompletedAt { get; private set; }
}
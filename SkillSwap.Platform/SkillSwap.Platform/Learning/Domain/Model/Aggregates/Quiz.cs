using SkillSwap.Platform.Learning.Domain.Model.ValueObjects;
using TutorId = SkillSwap.Platform.Discovery.Domain.Model.ValueObjects.TutorId;

namespace SkillSwap.Platform.Learning.Domain.Model.Aggregates;

public partial class Quiz
{
    public Quiz()
    {
        TutorId = new TutorId();
        Course = string.Empty;
        Title = string.Empty;
        Description = string.Empty;
        Questions = [];
        Status = "draft";
        CreatedAt = DateTime.UtcNow;
    }
    
    public int Id { get; private set; }
    public TutorId TutorId { get; private set; }
    public string Course { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<Question> Questions { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void updateInformation(string course, string title, string description, List<Question> questions)
    {
        this.Course = course;
        this.Title = title;
        this.Description = description;
        this.Questions = questions;
    }

    public void addQuestion(string question, string[] answers, int correctAnswers)
    {
        if (question == null || question.Length == 0)
        {
            throw new ArgumentNullException(nameof(question));
        }
        if (answers == null)
        {
            throw new ArgumentException("Answers cannot be null");
        }
        if (answers.Length != 4)
        {
            throw new ArgumentException("Answers must contain exactly 4 answers");
        }
        if (correctAnswers < 0 || correctAnswers >= answers.Length)
        {
            throw new ArithmeticException("CorrectAnswers must be between 0 and 3");
        }
        
        Question q = new Question(question, answers, correctAnswers);
        this.Questions.Add(q);
    }

    public void deleteQuestion(int questionIndex)
    {
        if (questionIndex < 0 || questionIndex >= this.Questions.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(questionIndex), "Índice fuera de rango.");
        }
        this.Questions.RemoveAt(questionIndex);
    }
}
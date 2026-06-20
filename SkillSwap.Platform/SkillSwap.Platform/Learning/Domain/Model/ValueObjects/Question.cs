namespace SkillSwap.Platform.Learning.Domain.Model.ValueObjects;

public class Question
{
    public string QuestionString { get; private set; }
    public string[] Answers { get; private set; }
    public int CorrectAnswer { get; private set; }

    public Question(string questionString, string[] answers, int correctAnswer)
    {
        QuestionString = questionString;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
    
    public void UpdateInformation(string questionString, string[] answers, int correctAnswer)
    {
        if (answers.Length != 4)
        {
            throw new ArgumentException("You must provide 4 answers");
        }
        if (correctAnswer < 0 || correctAnswer >= answers.Length)
        {
            throw new ArgumentException("Correct answer should be between 0 and 3");

        }
        QuestionString = questionString;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}
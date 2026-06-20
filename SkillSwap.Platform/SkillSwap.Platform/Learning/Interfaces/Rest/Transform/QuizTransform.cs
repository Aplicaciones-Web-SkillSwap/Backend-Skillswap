namespace SkillSwap.Platform.Learning.Interfaces.Rest.Transform;

using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.ValueObjects;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

public static class QuizTransform
{
    public static QuizResource ToResource(Quiz entity)
    {
        return new QuizResource(
            entity.Id,
            entity.Title,
            entity.Course,
            entity.Description,
            entity.TutorId.UserId,
            entity.Questions.Select(q => new QuestionResource(
                q.QuestionString,
                q.Answers,
                q.CorrectAnswer)).ToList()
        );
    }
}
using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.ValueObjects;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Learning.Interfaces.Rest.Transform;

public static class QuestionTransform
{
    public static QuestionResource ToResource(Question question)
    {
        return new QuestionResource(
            question.QuestionString,
            question.Answers,
            question.CorrectAnswer
        );
    }
}
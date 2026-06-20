using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Learning.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="QuizAttempt" /> aggregate into a
///     <see cref="QuizAttemptResource" />.
/// </summary>
public static class QuizAttemptResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="QuizAttempt" /> aggregate to its <see cref="QuizAttemptResource" /> representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="QuizAttempt" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A <see cref="QuizAttemptResource" /> object representing the provided attempt.
    /// </returns>
    public static QuizAttemptResource ToResourceFromEntity(QuizAttempt entity)
    {
        return new QuizAttemptResource(
            entity.Id,
            entity.QuizId,
            entity.AttemptLearnerId.UserId,
            entity.SessionId,
            entity.SelectedAnswers,
            entity.Score,
            entity.Total,
            entity.CompletedAt);
    }
}
using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Learning.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="SubmitQuizAttemptResource" /> into a
///     <see cref="SubmitQuizAttemptCommand" />.
/// </summary>
public static class SubmitQuizAttemptCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="SubmitQuizAttemptResource" /> to a <see cref="SubmitQuizAttemptCommand" />.
    /// </summary>
    /// <param name="quizId">
    ///     The unique identifier of the quiz being attempted.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="SubmitQuizAttemptResource" /> containing the attempt data.
    /// </param>
    /// <returns>
    ///     A new <see cref="SubmitQuizAttemptCommand" /> instance.
    /// </returns>
    public static SubmitQuizAttemptCommand ToCommandFromResource(int quizId, SubmitQuizAttemptResource resource)
    {
        return new SubmitQuizAttemptCommand(
            quizId,
            resource.LearnerId,
            resource.SessionId,
            resource.SelectedAnswers);
    }
}
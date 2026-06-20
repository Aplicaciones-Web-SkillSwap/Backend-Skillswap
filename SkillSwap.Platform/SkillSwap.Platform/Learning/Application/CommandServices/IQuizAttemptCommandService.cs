using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Learning.Application.CommandServices;

/// <summary>
///     QuizAttempt command service interface
/// </summary>
public interface IQuizAttemptCommandService
{
    /// <summary>
    ///     Handle submit quiz attempt command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="SubmitQuizAttemptCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the created <see cref="QuizAttempt" />
    /// </returns>
    Task<Result<QuizAttempt>> Handle(SubmitQuizAttemptCommand command, CancellationToken cancellationToken);
}
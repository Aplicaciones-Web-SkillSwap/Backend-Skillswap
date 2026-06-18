using SkillSwap.Platform.Reputation.Domain.Model.Aggregates;
using SkillSwap.Platform.Reputation.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Reputation.Application.CommandServices;

/// <summary>
///     Review command service interface
/// </summary>
public interface IReviewCommandService
{
    /// <summary>
    ///     Handle create review command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateReviewCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the created <see cref="Review" />
    /// </returns>
    Task<Result<Review>> Handle(CreateReviewCommand command, CancellationToken cancellationToken);
}
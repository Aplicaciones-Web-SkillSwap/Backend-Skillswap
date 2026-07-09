using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Reputation.Application.CommandServices;
using SkillSwap.Platform.Reputation.Domain.Model;
using SkillSwap.Platform.Reputation.Domain.Model.Aggregates;
using SkillSwap.Platform.Reputation.Domain.Model.Commands;
using SkillSwap.Platform.Reputation.Domain.Repositories;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;
using SkillSwap.Platform.Workspace.Domain.Repositories;

namespace SkillSwap.Platform.Reputation.Application.Internal.CommandServices;

/// <summary>
///     Review command service
/// </summary>
/// <param name="reviewRepository">
///     Review repository
/// </param>
/// <param name="sessionRepository">
///     Workspace session repository, used to validate the review is tied to a real, completed
///     session where the reviewer was the learner.
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
/// <param name="localizer">
///     String localizer for error messages
/// </param>
public class ReviewCommandService(
    IReviewRepository reviewRepository,
    ISessionRepository sessionRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : IReviewCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<Review>> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
    {
        if (command.Rating is < 1 or > 5)
            return Result<Review>.Failure(ReputationError.InvalidRating,
                _localizer[nameof(ReputationError.InvalidRating)]);

        var session = await sessionRepository.FindByIdAsync(command.SessionId, cancellationToken);
        if (session is null)
            return Result<Review>.Failure(ReputationError.SessionNotFound,
                _localizer[nameof(ReputationError.SessionNotFound)]);

        if (!session.IsCompleted)
            return Result<Review>.Failure(ReputationError.SessionNotCompleted,
                _localizer[nameof(ReputationError.SessionNotCompleted)]);

        if (session.SessionLearnerId.UserId != command.ReviewerId)
            return Result<Review>.Failure(ReputationError.ReviewerNotSessionLearner,
                _localizer[nameof(ReputationError.ReviewerNotSessionLearner)]);

        if (session.SessionTutorId.UserId != command.TutorId)
            return Result<Review>.Failure(ReputationError.TutorMismatch,
                _localizer[nameof(ReputationError.TutorMismatch)]);

        var alreadyExists = await reviewRepository.ExistsByReviewerAndSessionAsync(
            command.ReviewerId, command.SessionId, cancellationToken);
        if (alreadyExists)
            return Result<Review>.Failure(ReputationError.DuplicateReview,
                _localizer[nameof(ReputationError.DuplicateReview)]);

        var review = new Review(command);
        try
        {
            await reviewRepository.AddAsync(review, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Review>.Success(review);
        }
        catch (OperationCanceledException)
        {
            return Result<Review>.Failure(ReputationError.OperationCancelled,
                _localizer[nameof(ReputationError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Review>.Failure(ReputationError.DatabaseError,
                _localizer[nameof(ReputationError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Review>.Failure(ReputationError.InternalServerError,
                _localizer[nameof(ReputationError.InternalServerError)]);
        }
    }
}
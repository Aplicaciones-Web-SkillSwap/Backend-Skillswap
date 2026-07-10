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
///     session the reviewer actually participated in (as either the learner or the tutor).
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

        var isLearner = session.SessionLearnerId.UserId == command.ReviewerId;
        var isTutor = session.SessionTutorId.UserId == command.ReviewerId;
        if (!isLearner && !isTutor)
            return Result<Review>.Failure(ReputationError.ReviewerNotSessionParticipant,
                _localizer[nameof(ReputationError.ReviewerNotSessionParticipant)]);

        var alreadyExists = await reviewRepository.ExistsByReviewerAndSessionAsync(
            command.ReviewerId, command.SessionId, cancellationToken);
        if (alreadyExists)
            return Result<Review>.Failure(ReputationError.DuplicateReview,
                _localizer[nameof(ReputationError.DuplicateReview)]);

        var resolvedCommand = command with
        {
            TutorId = session.SessionTutorId.UserId,
            LearnerId = session.SessionLearnerId.UserId
        };
        var review = new Review(resolvedCommand);
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
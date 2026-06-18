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

namespace SkillSwap.Platform.Reputation.Application.Internal.CommandServices;

/// <summary>
///     Review command service
/// </summary>
/// <param name="reviewRepository">
///     Review repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
/// <param name="localizer">
///     String localizer for error messages
/// </param>
public class ReviewCommandService(
    IReviewRepository reviewRepository,
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
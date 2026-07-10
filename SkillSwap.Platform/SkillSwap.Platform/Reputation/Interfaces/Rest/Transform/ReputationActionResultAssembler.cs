using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Reputation.Domain.Model;
using SkillSwap.Platform.Reputation.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;

namespace SkillSwap.Platform.Reputation.Interfaces.Rest.Transform;

public static class ReputationActionResultAssembler
{
    private static int ToStatusCodeFromReputationError(ReputationError error)
    {
        return error switch
        {
            ReputationError.ReviewNotFound => StatusCodes.Status404NotFound,
            ReputationError.InvalidRating => StatusCodes.Status400BadRequest,
            ReputationError.DuplicateReview => StatusCodes.Status409Conflict,
            ReputationError.SessionNotFound => StatusCodes.Status404NotFound,
            ReputationError.SessionNotCompleted => StatusCodes.Status400BadRequest,
            ReputationError.ReviewerNotSessionParticipant => StatusCodes.Status403Forbidden,
            ReputationError.OperationCancelled => StatusCodes.Status409Conflict,
            ReputationError.DatabaseError => StatusCodes.Status500InternalServerError,
            ReputationError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateReviewResult(
        ControllerBase controller,
        Result<Review> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Review, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromReputationError((ReputationError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetReviewByIdResult(
        ControllerBase controller,
        Review? review,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Review, IActionResult> successAction)
    {
        if (review is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromReputationError(ReputationError.ReviewNotFound),
                ReputationError.ReviewNotFound,
                errorLocalizer[nameof(ReputationError.ReviewNotFound)]
            );
        return successAction(review);
    }
}
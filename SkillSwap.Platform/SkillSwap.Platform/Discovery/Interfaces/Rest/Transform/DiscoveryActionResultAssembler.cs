using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Discovery.Domain.Model;
using SkillSwap.Platform.Discovery.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;

namespace SkillSwap.Platform.Discovery.Interfaces.Rest.Transform;

public static class DiscoveryActionResultAssembler
{
    private static int ToStatusCodeFromDiscoveryError(DiscoveryError error)
    {
        return error switch
        {
            DiscoveryError.TutorNotFound => StatusCodes.Status404NotFound,
            DiscoveryError.TutorAlreadyExists => StatusCodes.Status409Conflict,
            DiscoveryError.OperationCancelled => StatusCodes.Status409Conflict,
            DiscoveryError.DatabaseError => StatusCodes.Status500InternalServerError,
            DiscoveryError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateTutorResult(
        ControllerBase controller,
        Result<Tutor> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Tutor, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromDiscoveryError((DiscoveryError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromUpdateTutorResult(
        ControllerBase controller,
        Result<Tutor> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Tutor, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromDiscoveryError((DiscoveryError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetTutorByIdResult(
        ControllerBase controller,
        Tutor? tutor,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Tutor, IActionResult> successAction)
    {
        if (tutor is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromDiscoveryError(DiscoveryError.TutorNotFound),
                DiscoveryError.TutorNotFound,
                errorLocalizer[nameof(DiscoveryError.TutorNotFound)]
            );
        return successAction(tutor);
    }
}
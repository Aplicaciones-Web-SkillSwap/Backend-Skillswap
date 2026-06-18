using SkillSwap.Platform.Moderation.Domain.Model;
using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace SkillSwap.Platform.Moderation.Interfaces.Rest.Transform;

public static class ModerationActionResultAssembler
{
    // --- Helper for transforming ModerationError to StatusCode ---
    private static int ToStatusCodeFromModerationError(ModerationError error)
    {
        return error switch
        {
            ModerationError.ReportNotFound => StatusCodes.Status404NotFound,
            ModerationError.SanctionNotFound => StatusCodes.Status404NotFound,
            ModerationError.SelfReportNotAllowed => StatusCodes.Status400BadRequest,
            ModerationError.DuplicatePendingReport => StatusCodes.Status409Conflict,
            ModerationError.InvalidSanctionDuration => StatusCodes.Status400BadRequest,
            ModerationError.OperationCancelled => StatusCodes.Status409Conflict,
            ModerationError.DatabaseError => StatusCodes.Status500InternalServerError,
            ModerationError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest // Default
        };
    }

    // --- Specific Assembler Methods ---

    public static IActionResult ToActionResultFromCreateReportResult(
        ControllerBase controller,
        Result<Report> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Report, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromModerationError((ModerationError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromCloseReportResult(
        ControllerBase controller,
        Result<Report> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Report, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromModerationError((ModerationError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetReportByIdResult(
        ControllerBase controller,
        Report? report,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Report, IActionResult> successAction)
    {
        if (report is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromModerationError(ModerationError.ReportNotFound),
                ModerationError.ReportNotFound,
                errorLocalizer[nameof(ModerationError.ReportNotFound)]
            );
        return successAction(report);
    }

    public static IActionResult ToActionResultFromCreateSanctionResult(
        ControllerBase controller,
        Result<Sanction> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Sanction, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromModerationError((ModerationError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetSanctionByIdResult(
        ControllerBase controller,
        Sanction? sanction,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Sanction, IActionResult> successAction)
    {
        if (sanction is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromModerationError(ModerationError.SanctionNotFound),
                ModerationError.SanctionNotFound,
                errorLocalizer[nameof(ModerationError.SanctionNotFound)]
            );
        return successAction(sanction);
    }
}

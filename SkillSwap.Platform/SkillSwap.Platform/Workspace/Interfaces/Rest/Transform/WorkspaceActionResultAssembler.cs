using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using SkillSwap.Platform.Workspace.Domain.Model;
using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;

namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Transform;

public static class WorkspaceActionResultAssembler
{
    private static int ToStatusCodeFromWorkspaceError(WorkspaceError error)
    {
        return error switch
        {
            WorkspaceError.SessionNotFound => StatusCodes.Status404NotFound,
            WorkspaceError.MessageNotFound => StatusCodes.Status404NotFound,
            WorkspaceError.InvalidSessionStatus => StatusCodes.Status400BadRequest,
            WorkspaceError.NotSessionParticipant => StatusCodes.Status403Forbidden,
            WorkspaceError.NotYourTurn => StatusCodes.Status403Forbidden,
            WorkspaceError.PendingSessionAlreadyExists => StatusCodes.Status409Conflict,
            WorkspaceError.OperationCancelled => StatusCodes.Status409Conflict,
            WorkspaceError.DatabaseError => StatusCodes.Status500InternalServerError,
            WorkspaceError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateSessionResult(
        ControllerBase controller,
        Result<Session> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Session, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromWorkspaceError((WorkspaceError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromUpdateSessionStatusResult(
        ControllerBase controller,
        Result<Session> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Session, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromWorkspaceError((WorkspaceError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetSessionByIdResult(
        ControllerBase controller,
        Session? session,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Session, IActionResult> successAction)
    {
        if (session is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromWorkspaceError(WorkspaceError.SessionNotFound),
                WorkspaceError.SessionNotFound,
                errorLocalizer[nameof(WorkspaceError.SessionNotFound)]
            );
        return successAction(session);
    }

    public static IActionResult ToActionResultFromCreateMessageResult(
        ControllerBase controller,
        Result<Message> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Message, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromWorkspaceError((WorkspaceError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromDeleteMessageResult(
        ControllerBase controller,
        Result result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory)
    {
        if (result.IsSuccess) return new NoContentResult();
        var statusCode = ToStatusCodeFromWorkspaceError((WorkspaceError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Learning.Domain.Model;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;

namespace SkillSwap.Platform.Learning.Interfaces.Rest.Transform;

public static class LearningActionResultAssembler
{
    private static int ToStatusCodeFromLearningError(LearningError error)
    {
        return error switch
        {
            LearningError.QuizNotFound => StatusCodes.Status404NotFound,
            LearningError.DatabaseError => StatusCodes.Status500InternalServerError,
            LearningError.InvalidQuestion => StatusCodes.Status400BadRequest,
            LearningError.QuizAttemptNotFound => StatusCodes.Status404NotFound,
            LearningError.InvalidAnswerCount => StatusCodes.Status400BadRequest,
            LearningError.OperationCancelled => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }

    /// <summary>
    /// Método unificado para manejar resultados de tipo Result<T>.
    /// Funciona tanto para Create como para Update.
    /// </summary>
    public static IActionResult ToActionResultFromQuizResult<T>(
        ControllerBase controller,
        Result<T> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) 
            return successAction(result.Value!);

        var statusCode = ToStatusCodeFromLearningError((LearningError)result.Error!);
        
        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            controller.HttpContext, 
            statusCode, 
            result.Error!.ToString(), 
            result.Message
        );

        return new ObjectResult(problemDetails) { StatusCode = statusCode };
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;

/// <summary>
///     Extension methods over the built-in ASP.NET Core <see cref="ProblemDetailsFactory" />
///     to build a consistent error response from a domain error enum and a localized message.
/// </summary>
public static class ProblemDetailsFactoryExtensions
{
    /// <summary>
    ///     Creates an <see cref="IActionResult" /> wrapping a <see cref="Microsoft.AspNetCore.Mvc.ProblemDetails" />
    ///     object, using the given status code, domain error and message.
    /// </summary>
    /// <param name="problemDetailsFactory">The built-in problem details factory.</param>
    /// <param name="controller">The controller producing the response.</param>
    /// <param name="statusCode">The HTTP status code to use.</param>
    /// <param name="error">The domain error (enum) that caused the failure.</param>
    /// <param name="message">The localized error message.</param>
    /// <returns>An <see cref="IActionResult" /> with the problem details payload.</returns>
    public static IActionResult CreateProblemDetails(
        this ProblemDetailsFactory problemDetailsFactory,
        ControllerBase controller,
        int statusCode,
        Enum? error,
        string message)
    {
        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            title: error?.ToString() ?? "Error",
            detail: message
        );

        return new ObjectResult(problemDetails) { StatusCode = statusCode };
    }
}

using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SkillSwap.Platform.Iam.Application.CommandServices;
using SkillSwap.Platform.Iam.Domain.Model.ValueObjects;
using SkillSwap.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SkillSwap.Platform.Iam.Interfaces.Rest.Resources;
using SkillSwap.Platform.Iam.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/authentication")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints.")]
public class AuthenticationController(
    IUserCommandService userCommandService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Sign In", "Authenticate a user with username and password.", OperationId = "SignIn")]
    [SwaggerResponse(200, "The user was authenticated.", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(401, "Invalid username or password.")]
    public async Task<IActionResult> SignIn(SignInResource resource, CancellationToken cancellationToken)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(signInCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromSignInResult(
            this,
            result,
            problemDetailsFactory,
            authenticated => Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(
                authenticated.User, authenticated.Token))
        );
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation("Sign Up", "Register a new user with an institutional (.edu.pe) email.",
        OperationId = "SignUp")]
    [SwaggerResponse(200, "The user was created successfully.")]
    [SwaggerResponse(400, "The email is not a valid institutional (.edu.pe) email.")]
    [SwaggerResponse(409, "The username or email is already taken.")]
    public async Task<IActionResult> SignUp(SignUpResource resource, CancellationToken cancellationToken)
    {
        // Self-registration can only ever create Student accounts. Coordinator accounts
        // are created directly in the database, never through this endpoint — the client
        // cannot elevate its own role by submitting a different value here.
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource, UserRole.Student);
        var result = await userCommandService.Handle(signUpCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromSignUpResult(
            this,
            result,
            problemDetailsFactory,
            () => Ok(new { message = "User created successfully." })
        );
    }
}

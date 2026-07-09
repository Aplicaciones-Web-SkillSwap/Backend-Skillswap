using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Iam.Application.CommandServices;
using SkillSwap.Platform.Iam.Application.QueryServices;
using SkillSwap.Platform.Iam.Domain.Model.Commands;
using SkillSwap.Platform.Iam.Domain.Model.Queries;
using SkillSwap.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SkillSwap.Platform.Iam.Interfaces.Rest.Resources;
using SkillSwap.Platform.Iam.Interfaces.Rest.Transform;
using SkillSwap.Platform.Shared.Interfaces.Rest;
using SkillSwap.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User Endpoints.")]
public class UsersController(
    IUserQueryService userQueryService,
    IUserCommandService userCommandService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get User by Id", "Get a user by its unique identifier.", OperationId = "GetUserById")]
    [SwaggerResponse(200, "The user was found and returned.", typeof(UserResource))]
    [SwaggerResponse(404, "The user was not found.")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromGetUserByIdResult(
            this,
            user,
            problemDetailsFactory,
            errorLocalizer,
            foundUser => Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(foundUser))
        );
    }

    [HttpGet]
    [SwaggerOperation("Get All Users", "Get all registered users.", OperationId = "GetAllUsers")]
    [SwaggerResponse(200, "The users were found and returned.", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery, cancellationToken);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    [HttpPatch("{id:int}/bio")]
    [SwaggerOperation("Update User Bio", "Update the authenticated user's learner bio.", OperationId = "UpdateUserBio")]
    [SwaggerResponse(200, "The bio was updated.", typeof(UserResource))]
    [SwaggerResponse(403, "The authenticated user is not the owner of this profile.")]
    [SwaggerResponse(404, "The user was not found.")]
    public async Task<IActionResult> UpdateUserBio(int id, UpdateUserBioResource resource,
        CancellationToken cancellationToken)
    {
        var updateUserBioCommand = new UpdateUserBioCommand(id, resource.Bio, this.CurrentUserId());
        var result = await userCommandService.Handle(updateUserBioCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromUpdateUserBioResult(
            this,
            result,
            problemDetailsFactory,
            updatedUser => Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(updatedUser))
        );
    }
}

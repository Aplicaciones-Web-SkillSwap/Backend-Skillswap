using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using SkillSwap.Platform.Workspace.Application.CommandServices;
using SkillSwap.Platform.Workspace.Application.QueryServices;
using SkillSwap.Platform.Workspace.Domain.Model.Commands;
using SkillSwap.Platform.Workspace.Domain.Model.Queries;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Workspace.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Message Endpoints.")]
public class MessagesController(
    IMessageCommandService messageCommandService,
    IMessageQueryService messageQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("session/{sessionId:int}")]
    [SwaggerOperation("Get Messages by Session Id", "Get all messages for a specific session.", OperationId = "GetMessagesBySessionId")]
    [SwaggerResponse(200, "The messages were found and returned.", typeof(IEnumerable<MessageResource>))]
    public async Task<IActionResult> GetMessagesBySessionId(int sessionId, CancellationToken cancellationToken)
    {
        var getMessagesBySessionIdQuery = new GetMessagesBySessionIdQuery(sessionId);
        var messages = await messageQueryService.Handle(getMessagesBySessionIdQuery, cancellationToken);
        var messageResources = messages.Select(MessageResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(messageResources);
    }

    [HttpPost]
    [SwaggerOperation("Create Message", "Create a new message in a session.", OperationId = "CreateMessage")]
    [SwaggerResponse(201, "The message was created.", typeof(MessageResource))]
    [SwaggerResponse(400, "The message was not created.")]
    public async Task<IActionResult> CreateMessage(CreateMessageResource resource, CancellationToken cancellationToken)
    {
        var createMessageCommand = CreateMessageCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await messageCommandService.Handle(createMessageCommand, cancellationToken);

        return WorkspaceActionResultAssembler.ToActionResultFromCreateMessageResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdMessage => CreatedAtAction(nameof(GetMessagesBySessionId),
                new { sessionId = createdMessage.SessionId },
                MessageResourceFromEntityAssembler.ToResourceFromEntity(createdMessage))
        );
    }

    [HttpDelete("{messageId:int}")]
    [SwaggerOperation("Delete Message", "Delete a message by its unique identifier.", OperationId = "DeleteMessage")]
    [SwaggerResponse(204, "The message was deleted.")]
    [SwaggerResponse(404, "The message was not found.")]
    public async Task<IActionResult> DeleteMessage(int messageId, CancellationToken cancellationToken)
    {
        var deleteMessageCommand = new DeleteMessageCommand(messageId);
        var result = await messageCommandService.Handle(deleteMessageCommand, cancellationToken);

        return WorkspaceActionResultAssembler.ToActionResultFromDeleteMessageResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory
        );
    }
}
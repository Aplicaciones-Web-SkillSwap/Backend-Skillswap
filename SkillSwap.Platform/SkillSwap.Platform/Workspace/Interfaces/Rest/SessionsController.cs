using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SkillSwap.Platform.Shared.Interfaces.Rest;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using SkillSwap.Platform.Workspace.Application.CommandServices;
using SkillSwap.Platform.Workspace.Application.QueryServices;
using SkillSwap.Platform.Workspace.Domain.Model.Queries;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;
using SkillSwap.Platform.Workspace.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Workspace.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Session Endpoints.")]
public class SessionsController(
    ISessionCommandService sessionCommandService,
    ISessionQueryService sessionQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [SwaggerOperation("Get All Sessions", "Get all tutoring sessions.", OperationId = "GetAllSessions")]
    [SwaggerResponse(200, "The sessions were found and returned.", typeof(IEnumerable<SessionResource>))]
    public async Task<IActionResult> GetAllSessions(CancellationToken cancellationToken)
    {
        var getAllSessionsQuery = new GetAllSessionsQuery();
        var sessions = await sessionQueryService.Handle(getAllSessionsQuery, cancellationToken);
        var sessionResources = sessions.Select(SessionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(sessionResources);
    }

    [HttpGet("{sessionId:int}")]
    [SwaggerOperation("Get Session by Id", "Get a tutoring session by its unique identifier.", OperationId = "GetSessionById")]
    [SwaggerResponse(200, "The session was found and returned.", typeof(SessionResource))]
    [SwaggerResponse(404, "The session was not found.")]
    public async Task<IActionResult> GetSessionById(int sessionId, CancellationToken cancellationToken)
    {
        var getSessionByIdQuery = new GetSessionByIdQuery(sessionId);
        var session = await sessionQueryService.Handle(getSessionByIdQuery, cancellationToken);

        return WorkspaceActionResultAssembler.ToActionResultFromGetSessionByIdResult(
            this,
            session,
            _errorLocalizer,
            _problemDetailsFactory,
            foundSession => Ok(SessionResourceFromEntityAssembler.ToResourceFromEntity(foundSession))
        );
    }

    [HttpGet("learner/{learnerId:int}")]
    [SwaggerOperation("Get Sessions by Learner Id", "Get all sessions for a specific learner.", OperationId = "GetSessionsByLearnerId")]
    [SwaggerResponse(200, "The sessions were found and returned.", typeof(IEnumerable<SessionResource>))]
    public async Task<IActionResult> GetSessionsByLearnerId(int learnerId, CancellationToken cancellationToken)
    {
        var getSessionsByLearnerIdQuery = new GetSessionsByLearnerIdQuery(learnerId);
        var sessions = await sessionQueryService.Handle(getSessionsByLearnerIdQuery, cancellationToken);
        var sessionResources = sessions.Select(SessionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(sessionResources);
    }

    [HttpGet("tutor/{tutorId:int}")]
    [SwaggerOperation("Get Sessions by Tutor Id", "Get all sessions for a specific tutor.", OperationId = "GetSessionsByTutorId")]
    [SwaggerResponse(200, "The sessions were found and returned.", typeof(IEnumerable<SessionResource>))]
    public async Task<IActionResult> GetSessionsByTutorId(int tutorId, CancellationToken cancellationToken)
    {
        var getSessionsByTutorIdQuery = new GetSessionsByTutorIdQuery(tutorId);
        var sessions = await sessionQueryService.Handle(getSessionsByTutorIdQuery, cancellationToken);
        var sessionResources = sessions.Select(SessionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(sessionResources);
    }

    [HttpPost]
    [Authorize(Roles = "Student")]
    [SwaggerOperation("Create Session", "Create a new tutoring session.", OperationId = "CreateSession")]
    [SwaggerResponse(201, "The session was created.", typeof(SessionResource))]
    [SwaggerResponse(400, "The session was not created.")]
    public async Task<IActionResult> CreateSession(CreateSessionResource resource, CancellationToken cancellationToken)
    {
        var createSessionCommand = CreateSessionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await sessionCommandService.Handle(createSessionCommand, cancellationToken);

        return WorkspaceActionResultAssembler.ToActionResultFromCreateSessionResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdSession => CreatedAtAction(nameof(GetSessionById), new { sessionId = createdSession.Id },
                SessionResourceFromEntityAssembler.ToResourceFromEntity(createdSession))
        );
    }

    [HttpPatch("{sessionId:int}/status")]
    [Authorize(Roles = "Student")]
    [SwaggerOperation("Update Session Status", "Update the status of a tutoring session.", OperationId = "UpdateSessionStatus")]
    [SwaggerResponse(200, "The session status was updated.", typeof(SessionResource))]
    [SwaggerResponse(404, "The session was not found.")]
    public async Task<IActionResult> UpdateSessionStatus(int sessionId, UpdateSessionStatusResource resource,
        CancellationToken cancellationToken)
    {
        var updateSessionStatusCommand =
            UpdateSessionStatusCommandFromResourceAssembler.ToCommandFromResource(sessionId, resource, this.CurrentUserId());
        var result = await sessionCommandService.Handle(updateSessionStatusCommand, cancellationToken);

        return WorkspaceActionResultAssembler.ToActionResultFromUpdateSessionStatusResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            updatedSession => Ok(SessionResourceFromEntityAssembler.ToResourceFromEntity(updatedSession))
        );
    }
    
    [HttpPut("{sessionId:int}")]
    [SwaggerOperation("Update Session", "Update a session by its unique identifier.", OperationId = "UpdateSession")]
    [SwaggerResponse(200, "The session was updated.", typeof(SessionResource))]
    [SwaggerResponse(404, "The session was not found.")]
    public async Task<IActionResult> UpdateSession(int sessionId, UpdateSessionStatusResource resource,
        CancellationToken cancellationToken)
    {
        var updateSessionStatusCommand =
            UpdateSessionStatusCommandFromResourceAssembler.ToCommandFromResource(sessionId, resource, this.CurrentUserId());
        var result = await sessionCommandService.Handle(updateSessionStatusCommand, cancellationToken);

        return WorkspaceActionResultAssembler.ToActionResultFromUpdateSessionStatusResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            updatedSession => Ok(SessionResourceFromEntityAssembler.ToResourceFromEntity(updatedSession))
        );
    }
}
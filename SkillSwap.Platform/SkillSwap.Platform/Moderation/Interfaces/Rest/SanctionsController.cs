using System.Net.Mime;
using SkillSwap.Platform.Moderation.Application.CommandServices;
using SkillSwap.Platform.Moderation.Application.QueryServices;
using SkillSwap.Platform.Moderation.Domain.Model.Queries;
using SkillSwap.Platform.Moderation.Interfaces.Rest.Resources;
using SkillSwap.Platform.Moderation.Interfaces.Rest.Transform;
using SkillSwap.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Moderation.Interfaces.Rest;

[Authorize(Roles = "Coordinator")]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Sanction Endpoints.")]
public class SanctionsController(
    ISanctionCommandService sanctionCommandService,
    ISanctionQueryService sanctionQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{sanctionId:int}")]
    [SwaggerOperation("Get Sanction by Id", "Get a sanction by its unique identifier.",
        OperationId = "GetSanctionById")]
    [SwaggerResponse(200, "The sanction was found and returned.", typeof(SanctionResource))]
    [SwaggerResponse(404, "The sanction was not found.")]
    public async Task<IActionResult> GetSanctionById(int sanctionId, CancellationToken cancellationToken)
    {
        var getSanctionByIdQuery = new GetSanctionByIdQuery(sanctionId);
        var sanction = await sanctionQueryService.Handle(getSanctionByIdQuery, cancellationToken);

        return ModerationActionResultAssembler.ToActionResultFromGetSanctionByIdResult(
            this,
            sanction,
            _errorLocalizer,
            _problemDetailsFactory,
            foundSanction => Ok(SanctionResourceFromEntityAssembler.ToResourceFromEntity(foundSanction))
        );
    }

    [HttpPost]
    [SwaggerOperation("Create Sanction", "Create a new sanction for an existing report.",
        OperationId = "CreateSanction")]
    [SwaggerResponse(201, "The sanction was created.", typeof(SanctionResource))]
    [SwaggerResponse(400, "The sanction was not created.")]
    [SwaggerResponse(404, "The related report was not found.")]
    public async Task<IActionResult> CreateSanction(CreateSanctionResource resource,
        CancellationToken cancellationToken)
    {
        var createSanctionCommand = CreateSanctionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await sanctionCommandService.Handle(createSanctionCommand, cancellationToken);

        return ModerationActionResultAssembler.ToActionResultFromCreateSanctionResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdSanction => CreatedAtAction(nameof(GetSanctionById), new { sanctionId = createdSanction.Id },
                SanctionResourceFromEntityAssembler.ToResourceFromEntity(createdSanction))
        );
    }

    [HttpGet]
    [SwaggerOperation("Get All Sanctions", "Get all sanctions.", OperationId = "GetAllSanctions")]
    [SwaggerResponse(200, "The sanctions were found and returned.", typeof(IEnumerable<SanctionResource>))]
    public async Task<IActionResult> GetAllSanctions(CancellationToken cancellationToken)
    {
        var getAllSanctionsQuery = new GetAllSanctionsQuery();
        var sanctions = await sanctionQueryService.Handle(getAllSanctionsQuery, cancellationToken);
        var sanctionResources = sanctions.Select(SanctionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(sanctionResources);
    }

    [HttpGet("report/{reportId:int}")]
    [SwaggerOperation("Get Sanctions By Report Id", "Get all sanctions associated with a specific report.",
        OperationId = "GetSanctionsByReportId")]
    [SwaggerResponse(200, "The sanctions were found and returned.", typeof(IEnumerable<SanctionResource>))]
    public async Task<IActionResult> GetSanctionsByReportId(int reportId, CancellationToken cancellationToken)
    {
        var getSanctionsByReportIdQuery = new GetSanctionsByReportIdQuery(reportId);
        var sanctions = await sanctionQueryService.Handle(getSanctionsByReportIdQuery, cancellationToken);
        var sanctionResources = sanctions.Select(SanctionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(sanctionResources);
    }
}

using System.Net.Mime;
using SkillSwap.Platform.Moderation.Application.CommandServices;
using SkillSwap.Platform.Moderation.Application.QueryServices;
using SkillSwap.Platform.Moderation.Domain.Model.Commands;
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

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Report Endpoints.")]
public class ReportsController(
    IReportCommandService reportCommandService,
    IReportQueryService reportQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{reportId:int}")]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Get Report by Id", "Get a report by its unique identifier.", OperationId = "GetReportById")]
    [SwaggerResponse(200, "The report was found and returned.", typeof(ReportResource))]
    [SwaggerResponse(404, "The report was not found.")]
    public async Task<IActionResult> GetReportById(int reportId, CancellationToken cancellationToken)
    {
        var getReportByIdQuery = new GetReportByIdQuery(reportId);
        var report = await reportQueryService.Handle(getReportByIdQuery, cancellationToken);

        return ModerationActionResultAssembler.ToActionResultFromGetReportByIdResult(
            this,
            report,
            _errorLocalizer,
            _problemDetailsFactory,
            foundReport => Ok(ReportResourceFromEntityAssembler.ToResourceFromEntity(foundReport))
        );
    }

    [HttpPost]
    [SwaggerOperation("Create Report", "Create a new report.", OperationId = "CreateReport")]
    [SwaggerResponse(201, "The report was created.", typeof(ReportResource))]
    [SwaggerResponse(400, "The report was not created.")]
    [SwaggerResponse(409, "A pending report already exists between these users.")]
    public async Task<IActionResult> CreateReport(CreateReportResource resource, CancellationToken cancellationToken)
    {
        var createReportCommand = CreateReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await reportCommandService.Handle(createReportCommand, cancellationToken);

        return ModerationActionResultAssembler.ToActionResultFromCreateReportResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdReport => CreatedAtAction(nameof(GetReportById), new { reportId = createdReport.Id },
                ReportResourceFromEntityAssembler.ToResourceFromEntity(createdReport))
        );
    }

    [HttpGet]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Get All Reports", "Get all reports.", OperationId = "GetAllReports")]
    [SwaggerResponse(200, "The reports were found and returned.", typeof(IEnumerable<ReportResource>))]
    public async Task<IActionResult> GetAllReports(CancellationToken cancellationToken)
    {
        var getAllReportsQuery = new GetAllReportsQuery();
        var reports = await reportQueryService.Handle(getAllReportsQuery, cancellationToken);
        var reportResources = reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(reportResources);
    }

    [HttpGet("reported-user/{reportedUserId:int}")]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Get Reports By Reported User", "Get all reports targeting a specific reported user.",
        OperationId = "GetReportsByReportedUser")]
    [SwaggerResponse(200, "The reports were found and returned.", typeof(IEnumerable<ReportResource>))]
    public async Task<IActionResult> GetReportsByReportedUser(int reportedUserId, CancellationToken cancellationToken)
    {
        var getReportsByReportedUserQuery = new GetReportsByReportedUserQuery(reportedUserId);
        var reports = await reportQueryService.Handle(getReportsByReportedUserQuery, cancellationToken);
        var reportResources = reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(reportResources);
    }

    [HttpPatch("{reportId:int}/close")]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Close Report", "Marks a report as resolved and closed.", OperationId = "CloseReport")]
    [SwaggerResponse(200, "The report was closed.", typeof(ReportResource))]
    [SwaggerResponse(404, "The report was not found.")]
    public async Task<IActionResult> CloseReport(int reportId, CancellationToken cancellationToken)
    {
        var closeReportCommand = new CloseReportCommand(reportId);
        var result = await reportCommandService.Handle(closeReportCommand, cancellationToken);

        return ModerationActionResultAssembler.ToActionResultFromCloseReportResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            closedReport => Ok(ReportResourceFromEntityAssembler.ToResourceFromEntity(closedReport))
        );
    }
}

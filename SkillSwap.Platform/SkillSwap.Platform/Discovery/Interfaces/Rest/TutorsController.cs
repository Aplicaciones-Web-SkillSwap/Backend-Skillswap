using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Discovery.Application.CommandServices;
using SkillSwap.Platform.Discovery.Application.QueryServices;
using SkillSwap.Platform.Discovery.Domain.Model.Commands;
using SkillSwap.Platform.Discovery.Domain.Model.Queries;
using SkillSwap.Platform.Discovery.Interfaces.Rest.Resources;
using SkillSwap.Platform.Discovery.Interfaces.Rest.Transform;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Discovery.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Tutor Endpoints.")]
public class TutorsController(
    ITutorCommandService tutorCommandService,
    ITutorQueryService tutorQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [SwaggerOperation("Get All Tutors", "Get all tutor profiles.", OperationId = "GetAllTutors")]
    [SwaggerResponse(200, "The tutors were found and returned.", typeof(IEnumerable<TutorResource>))]
    public async Task<IActionResult> GetAllTutors(CancellationToken cancellationToken)
    {
        var getAllTutorsQuery = new GetAllTutorsQuery();
        var tutors = await tutorQueryService.Handle(getAllTutorsQuery, cancellationToken);
        var tutorResources = tutors.Select(TutorResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tutorResources);
    }

    [HttpGet("{tutorId:int}")]
    [SwaggerOperation("Get Tutor by Id", "Get a tutor profile by its unique identifier.", OperationId = "GetTutorById")]
    [SwaggerResponse(200, "The tutor was found and returned.", typeof(TutorResource))]
    [SwaggerResponse(404, "The tutor was not found.")]
    public async Task<IActionResult> GetTutorById(int tutorId, CancellationToken cancellationToken)
    {
        var getTutorByIdQuery = new GetTutorByIdQuery(tutorId);
        var tutor = await tutorQueryService.Handle(getTutorByIdQuery, cancellationToken);

        return DiscoveryActionResultAssembler.ToActionResultFromGetTutorByIdResult(
            this,
            tutor,
            _errorLocalizer,
            _problemDetailsFactory,
            foundTutor => Ok(TutorResourceFromEntityAssembler.ToResourceFromEntity(foundTutor))
        );
    }

    [HttpGet("user/{userId:int}")]
    [SwaggerOperation("Get Tutor by User Id", "Get a tutor profile by its associated user identifier.", OperationId = "GetTutorByUserId")]
    [SwaggerResponse(200, "The tutor was found and returned.", typeof(TutorResource))]
    [SwaggerResponse(404, "The tutor was not found.")]
    public async Task<IActionResult> GetTutorByUserId(int userId, CancellationToken cancellationToken)
    {
        var getTutorByUserIdQuery = new GetTutorByUserIdQuery(userId);
        var tutor = await tutorQueryService.Handle(getTutorByUserIdQuery, cancellationToken);

        return DiscoveryActionResultAssembler.ToActionResultFromGetTutorByIdResult(
            this,
            tutor,
            _errorLocalizer,
            _problemDetailsFactory,
            foundTutor => Ok(TutorResourceFromEntityAssembler.ToResourceFromEntity(foundTutor))
        );
    }

    [HttpGet("skill/{skill}")]
    [SwaggerOperation("Get Tutors by Skill", "Get all tutor profiles that offer a specific skill.", OperationId = "GetTutorsBySkill")]
    [SwaggerResponse(200, "The tutors were found and returned.", typeof(IEnumerable<TutorResource>))]
    public async Task<IActionResult> GetTutorsBySkill(string skill, CancellationToken cancellationToken)
    {
        var getTutorsBySkillQuery = new GetTutorsBySkillQuery(skill);
        var tutors = await tutorQueryService.Handle(getTutorsBySkillQuery, cancellationToken);
        var tutorResources = tutors.Select(TutorResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tutorResources);
    }

    [HttpPost]
    [SwaggerOperation("Create Tutor", "Register a new tutor profile.", OperationId = "CreateTutor")]
    [SwaggerResponse(201, "The tutor was created.", typeof(TutorResource))]
    [SwaggerResponse(409, "A tutor profile already exists for this user.")]
    public async Task<IActionResult> CreateTutor(CreateTutorResource resource, CancellationToken cancellationToken)
    {
        var createTutorCommand = CreateTutorCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await tutorCommandService.Handle(createTutorCommand, cancellationToken);

        return DiscoveryActionResultAssembler.ToActionResultFromCreateTutorResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdTutor => CreatedAtAction(nameof(GetTutorById), new { tutorId = createdTutor.Id },
                TutorResourceFromEntityAssembler.ToResourceFromEntity(createdTutor))
        );
    }

    [HttpPut("{tutorId:int}")]
    [SwaggerOperation("Update Tutor", "Update an existing tutor profile.", OperationId = "UpdateTutor")]
    [SwaggerResponse(200, "The tutor was updated.", typeof(TutorResource))]
    [SwaggerResponse(404, "The tutor was not found.")]
    public async Task<IActionResult> UpdateTutor(int tutorId, UpdateTutorResource resource,
        CancellationToken cancellationToken)
    {
        var updateTutorCommand = UpdateTutorCommandFromResourceAssembler.ToCommandFromResource(tutorId, resource);
        var result = await tutorCommandService.Handle(updateTutorCommand, cancellationToken);

        return DiscoveryActionResultAssembler.ToActionResultFromUpdateTutorResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            updatedTutor => Ok(TutorResourceFromEntityAssembler.ToResourceFromEntity(updatedTutor))
        );
    }
}
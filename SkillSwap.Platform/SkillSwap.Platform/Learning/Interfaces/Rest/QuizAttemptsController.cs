using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Learning.Application.CommandServices;
using SkillSwap.Platform.Learning.Application.QueryServices;
using SkillSwap.Platform.Learning.Domain.Model.Queries;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;
using SkillSwap.Platform.Learning.Interfaces.Rest.Transform;
using SkillSwap.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SkillSwap.Platform.Shared.Interfaces.Rest;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Learning.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Quiz Attempt Endpoints.")]
public class QuizAttemptsController(
    IQuizAttemptCommandService quizAttemptCommandService,
    IQuizAttemptQueryService quizAttemptQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpPost("quiz/{quizId:int}")]
    [Authorize(Roles = "Student")]
    [SwaggerOperation("Submit Quiz Attempt", "Submit a learner's answers for a quiz and get the scored result.", OperationId = "SubmitQuizAttempt")]
    [SwaggerResponse(201, "The attempt was submitted and scored.", typeof(QuizAttemptResource))]
    [SwaggerResponse(400, "The number of answers does not match the number of questions.")]
    [SwaggerResponse(404, "The quiz was not found.")]
    public async Task<IActionResult> SubmitQuizAttempt(int quizId, SubmitQuizAttemptResource resource,
        CancellationToken cancellationToken)
    {
        var command = SubmitQuizAttemptCommandFromResourceAssembler.ToCommandFromResource(quizId, resource, this.CurrentUserId());
        var result = await quizAttemptCommandService.Handle(command, cancellationToken);

        return LearningActionResultAssembler.ToActionResultFromQuizResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdAttempt => CreatedAtAction(nameof(GetQuizAttemptById), new { attemptId = createdAttempt.Id },
                QuizAttemptResourceFromEntityAssembler.ToResourceFromEntity(createdAttempt))
        );
    }

    [HttpGet("{attemptId:int}")]
    [SwaggerOperation("Get Quiz Attempt by Id", "Get a specific quiz attempt by its identifier.", OperationId = "GetQuizAttemptById")]
    [SwaggerResponse(200, "The attempt was found and returned.", typeof(QuizAttemptResource))]
    [SwaggerResponse(404, "The attempt was not found.")]
    public async Task<IActionResult> GetQuizAttemptById(int attemptId, CancellationToken cancellationToken)
    {
        var query = new GetQuizAttemptByIdQuery(attemptId);
        var attempt = await quizAttemptQueryService.Handle(query, cancellationToken);

        return attempt is null ? NotFound() : Ok(QuizAttemptResourceFromEntityAssembler.ToResourceFromEntity(attempt));
    }

    [HttpGet("learner/{learnerId:int}")]
    [SwaggerOperation("Get Quiz Attempts by Learner Id", "Get all quiz attempts made by a specific learner.", OperationId = "GetQuizAttemptsByLearnerId")]
    [SwaggerResponse(200, "The attempts were found and returned.", typeof(IEnumerable<QuizAttemptResource>))]
    public async Task<IActionResult> GetQuizAttemptsByLearnerId(int learnerId, CancellationToken cancellationToken)
    {
        var query = new GetQuizAttemptsByLearnerIdQuery(learnerId);
        var attempts = await quizAttemptQueryService.Handle(query, cancellationToken);
        var resources = attempts.Select(QuizAttemptResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("quiz/{quizId:int}")]
    [SwaggerOperation("Get Quiz Attempts by Quiz Id", "Get all attempts made for a specific quiz.", OperationId = "GetQuizAttemptsByQuizId")]
    [SwaggerResponse(200, "The attempts were found and returned.", typeof(IEnumerable<QuizAttemptResource>))]
    public async Task<IActionResult> GetQuizAttemptsByQuizId(int quizId, CancellationToken cancellationToken)
    {
        var query = new GetQuizAttemptsByQuizIdQuery(quizId);
        var attempts = await quizAttemptQueryService.Handle(query, cancellationToken);
        var resources = attempts.Select(QuizAttemptResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Learning.Application.CommandServices;
using SkillSwap.Platform.Learning.Application.QueryServices;
using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Domain.Model.Queries;
using SkillSwap.Platform.Learning.Domain.Model.ValueObjects;
using SkillSwap.Platform.Learning.Interfaces.Rest.Resources;
using SkillSwap.Platform.Learning.Interfaces.Rest.Transform;
using SkillSwap.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Learning.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Quiz Endpoints.")]
public class QuizzesController(
    IQuizCommandService quizCommandService,
    IQuizQueryService quizQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    [HttpGet]
    [SwaggerOperation("Get All Quizzes", "Retrieve a list of all available quizzes.", OperationId = "GetAllQuizzes")]
    [SwaggerResponse(200, "The quizzes were retrieved.", typeof(IEnumerable<QuizResource>))]
    public async Task<IActionResult> GetAllQuizzes(CancellationToken cancellationToken)
    {
        var query = new GetAllQuizzesQuery();
        var quizzes = await quizQueryService.Handle(query, cancellationToken);
        
        var resources = quizzes.Select(QuizTransform.ToResource);
    
        return Ok(resources);
    }

    [HttpPost]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Create Quiz", "Register a new quiz.", OperationId = "CreateQuiz")]
    [SwaggerResponse(201, "The quiz was created.", typeof(QuizResource))]
    public async Task<IActionResult> CreateQuiz(CreateQuizResource resource, CancellationToken cancellationToken)
    {
        var command = CreateQuizCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await quizCommandService.Handle(command, cancellationToken);
        
        return LearningActionResultAssembler.ToActionResultFromQuizResult(
            this, 
            result, 
            _errorLocalizer, 
            _problemDetailsFactory,
            createdQuiz => CreatedAtAction(nameof(GetQuizById), new { quizId = createdQuiz.Id }, 
                QuizTransform.ToResource(createdQuiz))
        );
    }

    [HttpPost("{quizId:int}/questions")]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Add Question", "Add a new question to a quiz.", OperationId = "AddQuestion")]
    public async Task<IActionResult> AddQuestion(int quizId, AddQuestionResource resource, CancellationToken cancellationToken)
    {
        var command = AddQuestionCommandFromResourceAssembler.ToCommandFromResource(quizId, resource);
        var result = await quizCommandService.Handle(command, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpGet("{quizId:int}")]
    [SwaggerOperation("Get Quiz by Id", "Get quiz details by its identifier.", OperationId = "GetQuizById")]
    public async Task<IActionResult> GetQuizById(int quizId, CancellationToken cancellationToken)
    {
        var query = new GetQuizByIdQuery(quizId);
        var quiz = await quizQueryService.Handle(query, cancellationToken);
        
        return quiz is null ? NotFound() : Ok(QuizTransform.ToResource(quiz));
    }

    [HttpDelete("{quizId:int}")]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Delete Quiz", "Delete an existing quiz.", OperationId = "DeleteQuiz")]
    public async Task<IActionResult> DeleteQuiz(int quizId, CancellationToken cancellationToken)
    {
        var command = new DeleteQuizCommand(quizId);
        var result = await quizCommandService.Handle(command, cancellationToken);
        
        return result.IsSuccess ? NoContent() : NotFound(result.Error);
    }
    
    [HttpDelete("{quizId:int}/questions/{questionId:int}")]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Remove Question", "Removes a question from a specific quiz.", OperationId = "RemoveQuestionFromQuiz")]
    [SwaggerResponse(204, "The question was removed.")]
    [SwaggerResponse(404, "The quiz or question was not found.")]
    public async Task<IActionResult> RemoveQuestionFromQuiz(int quizId, int questionId, CancellationToken cancellationToken)
    {
        var command = new RemoveQuestionFromQuizCommand(quizId, questionId);
        var result = await quizCommandService.Handle(command, cancellationToken);
        
        return LearningActionResultAssembler.ToActionResultFromQuizResult(
            this, 
            result, 
            _errorLocalizer, 
            _problemDetailsFactory,
            _ => NoContent()
        );
    }
    
    [HttpPut("{quizId:int}")]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Update Quiz", "Update an existing quiz's basic information.", OperationId = "UpdateQuiz")]
    [SwaggerResponse(200, "The quiz was updated.", typeof(QuizResource))]
    [SwaggerResponse(404, "The quiz was not found.")]
    public async Task<IActionResult> UpdateQuiz(int quizId, UpdateQuizInfoResource resource, CancellationToken cancellationToken)
    {
        var command = UpdateQuizCommandFromResourceAssembler.ToCommandFromResource(quizId, resource);
        
        var result = await quizCommandService.Handle(command, cancellationToken);
        
        return LearningActionResultAssembler.ToActionResultFromQuizResult(
            this, 
            result,
            _errorLocalizer, 
            _problemDetailsFactory,
            updatedQuiz => Ok(QuizTransform.ToResource((Quiz)updatedQuiz)) 
        );
    }
    
    [HttpPut("{quizId:int}/questions/{questionId:int}")]
    [Authorize(Roles = "Coordinator")]
    [SwaggerOperation("Update Question", "Update details of a specific question in a quiz.", OperationId = "UpdateQuestion")]
    [SwaggerResponse(200, "The question was updated.", typeof(QuestionResource))]
    [SwaggerResponse(404, "The quiz or question was not found.")]
    public async Task<IActionResult> UpdateQuestion(int quizId, int questionId, [FromBody] UpdateQuestionResource resource, CancellationToken cancellationToken)
    {
        var command = UpdateQuestionCommandFromResourceAssembler.ToCommandFromResource(quizId, questionId, resource);
        var result = await quizCommandService.Handle(command, cancellationToken);
        
        if (!result.IsSuccess)
        {
            return LearningActionResultAssembler.ToActionResultFromQuizResult(
                this, result, _errorLocalizer, _problemDetailsFactory, _ => Ok() // La lambda aquí no importa
            );
        }
        
        return Ok(); 
    }
}
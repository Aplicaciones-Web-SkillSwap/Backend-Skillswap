using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Reputation.Application.CommandServices;
using SkillSwap.Platform.Reputation.Application.QueryServices;
using SkillSwap.Platform.Reputation.Domain.Model.Queries;
using SkillSwap.Platform.Reputation.Interfaces.Rest.Resources;
using SkillSwap.Platform.Reputation.Interfaces.Rest.Transform;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Reputation.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Review Endpoints.")]
public class ReviewsController(
    IReviewCommandService reviewCommandService,
    IReviewQueryService reviewQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [SwaggerOperation("Get All Reviews", "Get all reviews.", OperationId = "GetAllReviews")]
    [SwaggerResponse(200, "The reviews were found and returned.", typeof(IEnumerable<ReviewResource>))]
    public async Task<IActionResult> GetAllReviews(CancellationToken cancellationToken)
    {
        var getAllReviewsQuery = new GetAllReviewsQuery();
        var reviews = await reviewQueryService.Handle(getAllReviewsQuery, cancellationToken);
        var reviewResources = reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(reviewResources);
    }

    [HttpGet("{reviewId:int}")]
    [SwaggerOperation("Get Review by Id", "Get a review by its unique identifier.", OperationId = "GetReviewById")]
    [SwaggerResponse(200, "The review was found and returned.", typeof(ReviewResource))]
    [SwaggerResponse(404, "The review was not found.")]
    public async Task<IActionResult> GetReviewById(int reviewId, CancellationToken cancellationToken)
    {
        var getReviewByIdQuery = new GetReviewByIdQuery(reviewId);
        var review = await reviewQueryService.Handle(getReviewByIdQuery, cancellationToken);

        return ReputationActionResultAssembler.ToActionResultFromGetReviewByIdResult(
            this,
            review,
            _errorLocalizer,
            _problemDetailsFactory,
            foundReview => Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(foundReview))
        );
    }

    [HttpGet("tutor/{tutorId:int}")]
    [SwaggerOperation("Get Reviews by Tutor Id", "Get all reviews for a specific tutor.", OperationId = "GetReviewsByTutorId")]
    [SwaggerResponse(200, "The reviews were found and returned.", typeof(IEnumerable<ReviewResource>))]
    public async Task<IActionResult> GetReviewsByTutorId(int tutorId, CancellationToken cancellationToken)
    {
        var getReviewsByTutorIdQuery = new GetReviewsByTutorIdQuery(tutorId);
        var reviews = await reviewQueryService.Handle(getReviewsByTutorIdQuery, cancellationToken);
        var reviewResources = reviews.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(reviewResources);
    }

    [HttpPost]
    [SwaggerOperation("Create Review", "Create a new review for a tutor.", OperationId = "CreateReview")]
    [SwaggerResponse(201, "The review was created.", typeof(ReviewResource))]
    [SwaggerResponse(400, "The review was not created (invalid rating).")]
    [SwaggerResponse(409, "A review already exists for this session by this reviewer.")]
    public async Task<IActionResult> CreateReview(CreateReviewResource resource, CancellationToken cancellationToken)
    {
        var createReviewCommand = CreateReviewCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await reviewCommandService.Handle(createReviewCommand, cancellationToken);

        return ReputationActionResultAssembler.ToActionResultFromCreateReviewResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdReview => CreatedAtAction(nameof(GetReviewById), new { reviewId = createdReview.Id },
                ReviewResourceFromEntityAssembler.ToResourceFromEntity(createdReview))
        );
    }
}
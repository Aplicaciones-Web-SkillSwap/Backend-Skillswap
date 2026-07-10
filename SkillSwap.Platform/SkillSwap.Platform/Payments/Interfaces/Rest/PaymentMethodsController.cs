using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SkillSwap.Platform.Payments.Application.CommandServices;
using SkillSwap.Platform.Payments.Application.QueryServices;
using SkillSwap.Platform.Payments.Domain.Model.Queries;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;
using SkillSwap.Platform.Payments.Interfaces.Rest.Transform;
using SkillSwap.Platform.Shared.Interfaces.Rest;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Payments.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Payment Method Endpoints. All amounts here are fully simulated — no real payment mechanism is ever set up or contacted.")]
public class PaymentMethodsController(
    IPaymentMethodCommandService paymentMethodCommandService,
    IPaymentMethodQueryService paymentMethodQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("me")]
    [SwaggerOperation("Get My Payment Method", "Get the authenticated user's saved payment method, if any.", OperationId = "GetMyPaymentMethod")]
    [SwaggerResponse(200, "The payment method was found and returned.", typeof(PaymentMethodResource))]
    [SwaggerResponse(404, "No saved payment method exists for this user.")]
    public async Task<IActionResult> GetMyPaymentMethod(CancellationToken cancellationToken)
    {
        var query = new GetPaymentMethodByUserIdQuery(this.CurrentUserId());
        var paymentMethod = await paymentMethodQueryService.Handle(query, cancellationToken);
        if (paymentMethod is null) return NotFound();
        return Ok(PaymentMethodResourceFromEntityAssembler.ToResourceFromEntity(paymentMethod));
    }

    [HttpPut("me")]
    [Authorize(Roles = "Student")]
    [SwaggerOperation("Save My Payment Method", "Create or replace the authenticated user's saved payment method (simulated — no real card/bank/wallet data is ever processed).", OperationId = "SaveMyPaymentMethod")]
    [SwaggerResponse(200, "The payment method was saved.", typeof(PaymentMethodResource))]
    [SwaggerResponse(400, "The payment method type is invalid.")]
    public async Task<IActionResult> SaveMyPaymentMethod(SavePaymentMethodResource resource, CancellationToken cancellationToken)
    {
        var command = SavePaymentMethodCommandFromResourceAssembler.ToCommandFromResource(resource, this.CurrentUserId());
        var result = await paymentMethodCommandService.Handle(command, cancellationToken);

        return PaymentsActionResultAssembler.ToActionResultFromSavePaymentMethodResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            savedMethod => Ok(PaymentMethodResourceFromEntityAssembler.ToResourceFromEntity(savedMethod))
        );
    }
}

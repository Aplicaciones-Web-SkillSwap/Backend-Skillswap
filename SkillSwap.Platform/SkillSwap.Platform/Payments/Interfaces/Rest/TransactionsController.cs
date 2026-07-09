using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Payments.Application.CommandServices;
using SkillSwap.Platform.Payments.Application.QueryServices;
using SkillSwap.Platform.Payments.Domain.Model.Queries;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;
using SkillSwap.Platform.Payments.Interfaces.Rest.Transform;
using SkillSwap.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SkillSwap.Platform.Shared.Interfaces.Rest;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Payments.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Transaction Endpoints.")]
public class TransactionsController(
    ITransactionCommandService transactionCommandService,
    ITransactionQueryService transactionQueryService,
    IDonationCommandService donationCommandService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [SwaggerOperation("Get All Transactions", "Get all transactions.", OperationId = "GetAllTransactions")]
    [SwaggerResponse(200, "The transactions were found and returned.", typeof(IEnumerable<TransactionResource>))]
    public async Task<IActionResult> GetAllTransactions(CancellationToken cancellationToken)
    {
        var getAllTransactionsQuery = new GetAllTransactionsQuery();
        var transactions = await transactionQueryService.Handle(getAllTransactionsQuery, cancellationToken);
        var transactionResources = transactions.Select(TransactionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(transactionResources);
    }

    [HttpGet("wallet/{walletId:int}")]
    [SwaggerOperation("Get Transactions by Wallet Id", "Get all transactions for a specific wallet.", OperationId = "GetTransactionsByWalletId")]
    [SwaggerResponse(200, "The transactions were found and returned.", typeof(IEnumerable<TransactionResource>))]
    public async Task<IActionResult> GetTransactionsByWalletId(int walletId, CancellationToken cancellationToken)
    {
        var getTransactionsByWalletIdQuery = new GetTransactionsByWalletIdQuery(walletId);
        var transactions = await transactionQueryService.Handle(getTransactionsByWalletIdQuery, cancellationToken);
        var transactionResources = transactions.Select(TransactionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(transactionResources);
    }

    [HttpPost]
    [SwaggerOperation("Create Transaction", "Create a new transaction.", OperationId = "CreateTransaction")]
    [SwaggerResponse(201, "The transaction was created.", typeof(TransactionResource))]
    [SwaggerResponse(400, "The transaction was not created (invalid amount).")]
    public async Task<IActionResult> CreateTransaction(CreateTransactionResource resource,
        CancellationToken cancellationToken)
    {
        var createTransactionCommand = CreateTransactionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await transactionCommandService.Handle(createTransactionCommand, cancellationToken);

        return PaymentsActionResultAssembler.ToActionResultFromCreateTransactionResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdTransaction => CreatedAtAction(nameof(GetTransactionsByWalletId),
                new { walletId = createdTransaction.WalletId },
                TransactionResourceFromEntityAssembler.ToResourceFromEntity(createdTransaction))
        );
    }

    [HttpPost("donate")]
    [Authorize(Roles = "Student")]
    [SwaggerOperation("Donate", "Make a donation from a student's wallet to a tutor's wallet, applying a 5% platform fee.", OperationId = "Donate")]
    [SwaggerResponse(200, "The donation was completed.", typeof(DonationResultResource))]
    [SwaggerResponse(400, "The donation was not completed (invalid amount or insufficient funds).")]
    [SwaggerResponse(404, "The sender's or receiver's wallet was not found.")]
    public async Task<IActionResult> Donate(DonateResource resource, CancellationToken cancellationToken)
    {
        var donateCommand = DonateCommandFromResourceAssembler.ToCommandFromResource(resource, this.CurrentUserId());
        var result = await donationCommandService.Handle(donateCommand, cancellationToken);

        return PaymentsActionResultAssembler.ToActionResultFromDonateResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            donationResult => Ok(DonationResultResourceFromEntityAssembler.ToResourceFromEntity(donationResult))
        );
    }
}
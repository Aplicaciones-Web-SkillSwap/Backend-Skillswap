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
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap.Platform.Payments.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Wallet Endpoints.")]
public class WalletsController(
    IWalletCommandService walletCommandService,
    IWalletQueryService walletQueryService,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessage> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    
    [HttpGet]
    [SwaggerOperation("Get All Wallets", "Get all wallets.", OperationId = "GetAllWallets")]
    [SwaggerResponse(200, "The wallets were found and returned.", typeof(IEnumerable<WalletResource>))]
    public async Task<IActionResult> GetAllWallets(CancellationToken cancellationToken)
    {
        var getAllWalletsQuery = new GetAllWalletsQuery();
        var wallets = await walletQueryService.Handle(getAllWalletsQuery, cancellationToken);
        var walletResources = wallets.Select(WalletResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(walletResources);
    }

    [HttpGet("{walletId:int}")]
    [SwaggerOperation("Get Wallet by Id", "Get a wallet by its unique identifier.", OperationId = "GetWalletById")]
    [SwaggerResponse(200, "The wallet was found and returned.", typeof(WalletResource))]
    [SwaggerResponse(404, "The wallet was not found.")]
    public async Task<IActionResult> GetWalletById(int walletId, CancellationToken cancellationToken)
    {
        var getWalletByIdQuery = new GetWalletByIdQuery(walletId);
        var wallet = await walletQueryService.Handle(getWalletByIdQuery, cancellationToken);

        return PaymentsActionResultAssembler.ToActionResultFromGetWalletByIdResult(
            this,
            wallet,
            _errorLocalizer,
            _problemDetailsFactory,
            foundWallet => Ok(WalletResourceFromEntityAssembler.ToResourceFromEntity(foundWallet))
        );
    }

    [HttpGet("user/{userId:int}")]
    [SwaggerOperation("Get Wallet by User Id", "Get a wallet by its owner's user identifier.", OperationId = "GetWalletByUserId")]
    [SwaggerResponse(200, "The wallet was found and returned.", typeof(WalletResource))]
    [SwaggerResponse(404, "The wallet was not found.")]
    public async Task<IActionResult> GetWalletByUserId(int userId, CancellationToken cancellationToken)
    {
        var getWalletByUserIdQuery = new GetWalletByUserIdQuery(userId);
        var wallet = await walletQueryService.Handle(getWalletByUserIdQuery, cancellationToken);

        return PaymentsActionResultAssembler.ToActionResultFromGetWalletByIdResult(
            this,
            wallet,
            _errorLocalizer,
            _problemDetailsFactory,
            foundWallet => Ok(WalletResourceFromEntityAssembler.ToResourceFromEntity(foundWallet))
        );
    }

    [HttpPost]
    [Authorize(Roles = "Student")]
    [SwaggerOperation("Create Wallet", "Create a new wallet for a user.", OperationId = "CreateWallet")]
    [SwaggerResponse(201, "The wallet was created.", typeof(WalletResource))]
    [SwaggerResponse(409, "A wallet already exists for this user.")]
    public async Task<IActionResult> CreateWallet(CreateWalletResource resource, CancellationToken cancellationToken)
    {
        var createWalletCommand = CreateWalletCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await walletCommandService.Handle(createWalletCommand, cancellationToken);

        return PaymentsActionResultAssembler.ToActionResultFromCreateWalletResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdWallet => CreatedAtAction(nameof(GetWalletById), new { walletId = createdWallet.Id },
                WalletResourceFromEntityAssembler.ToResourceFromEntity(createdWallet))
        );
    }

    [HttpPatch("{walletId:int}/add-funds")]
    [SwaggerOperation("Add Funds", "Add funds to an existing wallet.", OperationId = "AddFunds")]
    [SwaggerResponse(200, "The funds were added.", typeof(WalletResource))]
    [SwaggerResponse(404, "The wallet was not found.")]
    public async Task<IActionResult> AddFunds(int walletId, AddFundsResource resource,
        CancellationToken cancellationToken)
    {
        var addFundsCommand = AddFundsCommandFromResourceAssembler.ToCommandFromResource(walletId, resource);
        var result = await walletCommandService.Handle(addFundsCommand, cancellationToken);

        return PaymentsActionResultAssembler.ToActionResultFromAddFundsResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            updatedWallet => Ok(WalletResourceFromEntityAssembler.ToResourceFromEntity(updatedWallet))
        );
    }
}
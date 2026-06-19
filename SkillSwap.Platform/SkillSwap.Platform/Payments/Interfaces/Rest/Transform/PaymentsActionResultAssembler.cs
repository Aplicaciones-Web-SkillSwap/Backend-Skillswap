using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Payments.Domain.Model;
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SkillSwap.Platform.Shared.Resources.Errors;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

public static class PaymentsActionResultAssembler
{
    private static int ToStatusCodeFromPaymentsError(PaymentsError error)
    {
        return error switch
        {
            PaymentsError.WalletNotFound => StatusCodes.Status404NotFound,
            PaymentsError.WalletAlreadyExists => StatusCodes.Status409Conflict,
            PaymentsError.InsufficientFunds => StatusCodes.Status400BadRequest,
            PaymentsError.InvalidAmount => StatusCodes.Status400BadRequest,
            PaymentsError.SenderWalletNotFound => StatusCodes.Status404NotFound,
            PaymentsError.ReceiverWalletNotFound => StatusCodes.Status404NotFound,
            PaymentsError.OperationCancelled => StatusCodes.Status409Conflict,
            PaymentsError.DatabaseError => StatusCodes.Status500InternalServerError,
            PaymentsError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateWalletResult(
        ControllerBase controller,
        Result<Wallet> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Wallet, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromPaymentsError((PaymentsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromAddFundsResult(
        ControllerBase controller,
        Result<Wallet> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Wallet, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromPaymentsError((PaymentsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetWalletByIdResult(
        ControllerBase controller,
        Wallet? wallet,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Wallet, IActionResult> successAction)
    {
        if (wallet is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromPaymentsError(PaymentsError.WalletNotFound),
                PaymentsError.WalletNotFound,
                errorLocalizer[nameof(PaymentsError.WalletNotFound)]
            );
        return successAction(wallet);
    }

    public static IActionResult ToActionResultFromCreateTransactionResult(
        ControllerBase controller,
        Result<Transaction> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Transaction, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromPaymentsError((PaymentsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromDonateResult(
        ControllerBase controller,
        Result<DonationResult> result,
        IStringLocalizer<ErrorMessage> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<DonationResult, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCodeFromPaymentsError((PaymentsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
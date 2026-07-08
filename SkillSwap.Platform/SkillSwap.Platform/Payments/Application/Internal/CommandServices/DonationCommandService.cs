using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Payments.Application.CommandServices;
using SkillSwap.Platform.Payments.Domain.Model;
using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Payments.Domain.Repositories;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;

namespace SkillSwap.Platform.Payments.Application.Internal.CommandServices;

/// <summary>
///     Donation command service
/// </summary>
/// <remarks>
///     Orchestrates a donation between a student wallet and a tutor wallet,
///     applying a 5% platform fee, inside a single atomic database transaction.
/// </remarks>
/// <param name="walletRepository">
///     Wallet repository
/// </param>
/// <param name="transactionRepository">
///     Transaction repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
/// <param name="localizer">
///     String localizer for error messages
/// </param>
public class DonationCommandService(
    IWalletRepository walletRepository,
    ITransactionRepository transactionRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : IDonationCommandService
{
    private const decimal PlatformFeeRate = 0.05m;
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<DonationResult>> Handle(DonateCommand command, CancellationToken cancellationToken)
    {
        if (command.Amount <= 0)
            return Result<DonationResult>.Failure(PaymentsError.InvalidAmount,
                _localizer[nameof(PaymentsError.InvalidAmount)]);

        if (command.FromUserId == command.ToUserId)
            return Result<DonationResult>.Failure(PaymentsError.SelfDonationNotAllowed,
                _localizer[nameof(PaymentsError.SelfDonationNotAllowed)]);

        var senderWallet = await walletRepository.FindByUserIdAsync(command.FromUserId, cancellationToken);
        if (senderWallet is null)
            return Result<DonationResult>.Failure(PaymentsError.SenderWalletNotFound,
                _localizer[nameof(PaymentsError.SenderWalletNotFound)]);

        var receiverWallet = await walletRepository.FindByUserIdAsync(command.ToUserId, cancellationToken);
        if (receiverWallet is null)
            return Result<DonationResult>.Failure(PaymentsError.ReceiverWalletNotFound,
                _localizer[nameof(PaymentsError.ReceiverWalletNotFound)]);

        if (senderWallet.Balance < command.Amount)
            return Result<DonationResult>.Failure(PaymentsError.InsufficientFunds,
                _localizer[nameof(PaymentsError.InsufficientFunds)]);

        var platformFee = Math.Round(command.Amount * PlatformFeeRate, 2);
        var amountReceived = command.Amount - platformFee;

        DonationResult? donationResult = null;

        try
        {
            await unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                senderWallet.Deduct(command.Amount);
                receiverWallet.AddFunds(new Domain.Model.Commands.AddFundsCommand(receiverWallet.Id, amountReceived));

                walletRepository.Update(senderWallet);
                walletRepository.Update(receiverWallet);

                var transaction = new Domain.Model.Aggregates.Transaction(
                    new CreateTransactionCommand(
                        receiverWallet.Id,
                        amountReceived,
                        "donation",
                        command.Description));

                await transactionRepository.AddAsync(transaction, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);

                donationResult = new DonationResult(
                    transaction.Id,
                    command.Amount,
                    platformFee,
                    amountReceived,
                    senderWallet.Balance,
                    receiverWallet.Balance);
            }, cancellationToken);

            return Result<DonationResult>.Success(donationResult!);
        }
        catch (OperationCanceledException)
        {
            return Result<DonationResult>.Failure(PaymentsError.OperationCancelled,
                _localizer[nameof(PaymentsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<DonationResult>.Failure(PaymentsError.DatabaseError,
                _localizer[nameof(PaymentsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<DonationResult>.Failure(PaymentsError.InternalServerError,
                _localizer[nameof(PaymentsError.InternalServerError)]);
        }
    }
}
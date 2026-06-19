using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Payments.Application.CommandServices;
using SkillSwap.Platform.Payments.Domain.Model;
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Payments.Domain.Repositories;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;

namespace SkillSwap.Platform.Payments.Application.Internal.CommandServices;

/// <summary>
///     Wallet command service
/// </summary>
/// <param name="walletRepository">
///     Wallet repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
/// <param name="localizer">
///     String localizer for error messages
/// </param>
public class WalletCommandService(
    IWalletRepository walletRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : IWalletCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<Wallet>> Handle(CreateWalletCommand command, CancellationToken cancellationToken)
    {
        var alreadyExists = await walletRepository.ExistsByUserIdAsync(command.UserId, cancellationToken);
        if (alreadyExists)
            return Result<Wallet>.Failure(PaymentsError.WalletAlreadyExists,
                _localizer[nameof(PaymentsError.WalletAlreadyExists)]);

        var wallet = new Wallet(command);
        try
        {
            await walletRepository.AddAsync(wallet, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Wallet>.Success(wallet);
        }
        catch (OperationCanceledException)
        {
            return Result<Wallet>.Failure(PaymentsError.OperationCancelled,
                _localizer[nameof(PaymentsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Wallet>.Failure(PaymentsError.DatabaseError,
                _localizer[nameof(PaymentsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Wallet>.Failure(PaymentsError.InternalServerError,
                _localizer[nameof(PaymentsError.InternalServerError)]);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Wallet>> Handle(AddFundsCommand command, CancellationToken cancellationToken)
    {
        if (command.Amount <= 0)
            return Result<Wallet>.Failure(PaymentsError.InvalidAmount,
                _localizer[nameof(PaymentsError.InvalidAmount)]);

        var wallet = await walletRepository.FindByIdAsync(command.WalletId, cancellationToken);
        if (wallet is null)
            return Result<Wallet>.Failure(PaymentsError.WalletNotFound,
                _localizer[nameof(PaymentsError.WalletNotFound)]);

        wallet.AddFunds(command);
        try
        {
            walletRepository.Update(wallet);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Wallet>.Success(wallet);
        }
        catch (OperationCanceledException)
        {
            return Result<Wallet>.Failure(PaymentsError.OperationCancelled,
                _localizer[nameof(PaymentsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Wallet>.Failure(PaymentsError.DatabaseError,
                _localizer[nameof(PaymentsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Wallet>.Failure(PaymentsError.InternalServerError,
                _localizer[nameof(PaymentsError.InternalServerError)]);
        }
    }
}
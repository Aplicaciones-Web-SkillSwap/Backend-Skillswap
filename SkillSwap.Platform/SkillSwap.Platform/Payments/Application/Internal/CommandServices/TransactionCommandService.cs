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
///     Transaction command service
/// </summary>
/// <param name="transactionRepository">
///     Transaction repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
/// <param name="localizer">
///     String localizer for error messages
/// </param>
public class TransactionCommandService(
    ITransactionRepository transactionRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : ITransactionCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<Transaction>> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        if (command.Amount <= 0)
            return Result<Transaction>.Failure(PaymentsError.InvalidAmount,
                _localizer[nameof(PaymentsError.InvalidAmount)]);

        var transaction = new Transaction(command);
        try
        {
            await transactionRepository.AddAsync(transaction, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Transaction>.Success(transaction);
        }
        catch (OperationCanceledException)
        {
            return Result<Transaction>.Failure(PaymentsError.OperationCancelled,
                _localizer[nameof(PaymentsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Transaction>.Failure(PaymentsError.DatabaseError,
                _localizer[nameof(PaymentsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Transaction>.Failure(PaymentsError.InternalServerError,
                _localizer[nameof(PaymentsError.InternalServerError)]);
        }
    }
}
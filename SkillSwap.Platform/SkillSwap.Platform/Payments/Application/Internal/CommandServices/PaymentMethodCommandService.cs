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
///     Payment method command service
/// </summary>
/// <remarks>
///     A user may only have one saved payment method at a time; saving a new one replaces
///     whatever was saved before, rather than accumulating multiple entries.
/// </remarks>
public class PaymentMethodCommandService(
    IPaymentMethodRepository paymentMethodRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : IPaymentMethodCommandService
{
    private static readonly string[] ValidTypes = ["card", "bank", "yape"];

    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<PaymentMethod>> Handle(SavePaymentMethodCommand command, CancellationToken cancellationToken)
    {
        if (!ValidTypes.Contains(command.Type))
            return Result<PaymentMethod>.Failure(PaymentsError.InvalidPaymentMethodType,
                _localizer[nameof(PaymentsError.InvalidPaymentMethodType)]);

        try
        {
            var existing = await paymentMethodRepository.FindByUserIdAsync(command.UserId, cancellationToken);
            if (existing is not null)
            {
                existing.Replace(command);
                paymentMethodRepository.Update(existing);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<PaymentMethod>.Success(existing);
            }

            var paymentMethod = new PaymentMethod(command);
            await paymentMethodRepository.AddAsync(paymentMethod, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<PaymentMethod>.Success(paymentMethod);
        }
        catch (OperationCanceledException)
        {
            return Result<PaymentMethod>.Failure(PaymentsError.OperationCancelled,
                _localizer[nameof(PaymentsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<PaymentMethod>.Failure(PaymentsError.DatabaseError,
                _localizer[nameof(PaymentsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<PaymentMethod>.Failure(PaymentsError.InternalServerError,
                _localizer[nameof(PaymentsError.InternalServerError)]);
        }
    }
}

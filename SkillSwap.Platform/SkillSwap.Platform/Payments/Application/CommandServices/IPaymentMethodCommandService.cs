using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Payments.Application.CommandServices;

public interface IPaymentMethodCommandService
{
    Task<Result<PaymentMethod>> Handle(SavePaymentMethodCommand command, CancellationToken cancellationToken);
}

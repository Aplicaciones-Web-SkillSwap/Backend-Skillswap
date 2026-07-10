using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Queries;

namespace SkillSwap.Platform.Payments.Application.QueryServices;

public interface IPaymentMethodQueryService
{
    Task<PaymentMethod?> Handle(GetPaymentMethodByUserIdQuery query, CancellationToken cancellationToken);
}

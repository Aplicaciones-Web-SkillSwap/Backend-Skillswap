using SkillSwap.Platform.Payments.Application.QueryServices;
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Model.Queries;
using SkillSwap.Platform.Payments.Domain.Repositories;

namespace SkillSwap.Platform.Payments.Application.Internal.QueryServices;

public class PaymentMethodQueryService(IPaymentMethodRepository paymentMethodRepository) : IPaymentMethodQueryService
{
    public async Task<PaymentMethod?> Handle(GetPaymentMethodByUserIdQuery query, CancellationToken cancellationToken)
    {
        return await paymentMethodRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}

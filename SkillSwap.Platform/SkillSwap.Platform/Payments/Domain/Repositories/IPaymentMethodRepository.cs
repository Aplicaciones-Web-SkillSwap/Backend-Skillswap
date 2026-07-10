using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Shared.Domain.Repositories;

namespace SkillSwap.Platform.Payments.Domain.Repositories;

/// <summary>
///     Payment method repository interface
/// </summary>
public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod>
{
    /// <summary>
    ///     Find the saved payment method belonging to a user, if any.
    /// </summary>
    Task<PaymentMethod?> FindByUserIdAsync(int userId, CancellationToken cancellationToken);
}

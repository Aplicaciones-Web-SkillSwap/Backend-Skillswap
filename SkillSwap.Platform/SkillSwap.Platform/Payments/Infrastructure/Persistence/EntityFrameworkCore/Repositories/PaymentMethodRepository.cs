using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SkillSwap.Platform.Payments.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PaymentMethodRepository(AppDbContext context)
    : BaseRepository<PaymentMethod>(context), IPaymentMethodRepository
{
    public async Task<PaymentMethod?> FindByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await Context.Set<PaymentMethod>()
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }
}

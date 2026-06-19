using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SkillSwap.Platform.Payments.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Wallet repository implementation
/// </summary>
/// <param name="context">
///     The database context
/// </param>
public class WalletRepository(AppDbContext context)
    : BaseRepository<Wallet>(context), IWalletRepository
{
    /// <inheritdoc />
    public async Task<Wallet?> FindByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Wallet>()
            .FirstOrDefaultAsync(w => w.WalletOwnerId.UserId == userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Wallet>()
            .AnyAsync(w => w.WalletOwnerId.UserId == userId, cancellationToken);
    }
}
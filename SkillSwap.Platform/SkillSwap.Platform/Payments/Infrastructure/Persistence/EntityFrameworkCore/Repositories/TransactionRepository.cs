using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SkillSwap.Platform.Payments.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Transaction repository implementation
/// </summary>
/// <param name="context">
///     The database context
/// </param>
public class TransactionRepository(AppDbContext context)
    : BaseRepository<Transaction>(context), ITransactionRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Transaction>> FindByWalletIdAsync(int walletId, CancellationToken cancellationToken)
    {
        return await Context.Set<Transaction>()
            .Where(t => t.WalletId == walletId)
            .ToListAsync(cancellationToken);
    }
}
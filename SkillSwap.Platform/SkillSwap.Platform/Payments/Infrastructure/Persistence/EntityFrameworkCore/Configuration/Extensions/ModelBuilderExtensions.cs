using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Payments.Domain.Model.Aggregates;

namespace SkillSwap.Platform.Payments.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPaymentsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Wallet>().ToTable("wallets");
        builder.Entity<Wallet>().HasKey(w => w.Id);
        builder.Entity<Wallet>().Property(w => w.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Wallet>().OwnsOne(w => w.WalletOwnerId, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.UserId).HasColumnName("OwnerId");
        });
        builder.Entity<Wallet>().Property(w => w.Balance).IsRequired().HasColumnType("decimal(18,2)");

        builder.Entity<Transaction>().ToTable("transactions");
        builder.Entity<Transaction>().HasKey(t => t.Id);
        builder.Entity<Transaction>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Transaction>().Property(t => t.WalletId).IsRequired();
        builder.Entity<Transaction>().Property(t => t.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Entity<Transaction>().Property(t => t.Type).IsRequired().HasMaxLength(50);
        builder.Entity<Transaction>().Property(t => t.Description).IsRequired().HasMaxLength(500);
        builder.Entity<Transaction>().Property(t => t.CreatedAt).IsRequired();
    }
}
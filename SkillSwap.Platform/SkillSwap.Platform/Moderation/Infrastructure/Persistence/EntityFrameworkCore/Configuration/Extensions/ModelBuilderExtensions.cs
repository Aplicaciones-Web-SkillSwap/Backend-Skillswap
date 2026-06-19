using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace SkillSwap.Platform.Moderation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyModerationConfiguration(this ModelBuilder builder)
    {
        // Moderation Context

        builder.Entity<Report>().ToTable("reports");
        builder.Entity<Report>().HasKey(r => r.Id);
        builder.Entity<Report>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Report>().OwnsOne(r => r.ReporterUserId,
            v =>
            {
                v.WithOwner().HasForeignKey("Id");
                v.Property(p => p.UserId).HasColumnName("ReporterUserId");
            });
        builder.Entity<Report>().OwnsOne(r => r.ReportedUserId,
            v =>
            {
                v.WithOwner().HasForeignKey("Id");
                v.Property(p => p.UserId).HasColumnName("ReportedUserId");
            });
        builder.Entity<Report>().OwnsOne(r => r.ReportSessionId,
            v =>
            {
                v.WithOwner().HasForeignKey("Id");
                v.Property(p => p.Value).HasColumnName("SessionId");
            });
        builder.Entity<Report>().Property(r => r.Reason).IsRequired().HasMaxLength(500);
        builder.Entity<Report>().Property(r => r.Status).IsRequired().HasMaxLength(50);

        builder.Entity<Sanction>().ToTable("sanctions");
        builder.Entity<Sanction>().HasKey(s => s.Id);
        builder.Entity<Sanction>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Sanction>().OwnsOne(s => s.SanctionedUserId,
            v =>
            {
                v.WithOwner().HasForeignKey("Id");
                v.Property(p => p.UserId).HasColumnName("SanctionedUserId");
            });
        builder.Entity<Sanction>().Property(s => s.Type).IsRequired().HasMaxLength(50);
        builder.Entity<Sanction>().Property(s => s.Description).IsRequired().HasMaxLength(1000);
    }
}
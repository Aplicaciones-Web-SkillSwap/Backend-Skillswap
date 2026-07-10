using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Reputation.Domain.Model.Aggregates;

namespace SkillSwap.Platform.Reputation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyReputationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Review>().ToTable("reviews");
        builder.Entity<Review>().HasKey(r => r.Id);
        builder.Entity<Review>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Review>().OwnsOne(r => r.ReviewerUserId, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.UserId).HasColumnName("ReviewerUserId");
        });
        builder.Entity<Review>().OwnsOne(r => r.ReviewedTutorId, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.TutorId).HasColumnName("ReviewedTutorId");
        });
        builder.Entity<Review>().OwnsOne(r => r.ReviewedLearnerId, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.LearnerId).HasColumnName("ReviewedLearnerId");
        });
        builder.Entity<Review>().Property(r => r.SessionId).IsRequired();
        builder.Entity<Review>().Property(r => r.Rating).IsRequired();
        builder.Entity<Review>().Property(r => r.Comment).IsRequired().HasMaxLength(1000);
        builder.Entity<Review>().Property(r => r.ReviewedAt).IsRequired();
    }
}
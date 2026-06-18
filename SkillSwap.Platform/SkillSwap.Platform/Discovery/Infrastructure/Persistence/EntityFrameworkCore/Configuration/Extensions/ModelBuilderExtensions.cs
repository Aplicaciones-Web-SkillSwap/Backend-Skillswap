using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Discovery.Domain.Model.Aggregates;

namespace SkillSwap.Platform.Discovery.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDiscoveryConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Tutor>().ToTable("tutors");
        builder.Entity<Tutor>().HasKey(t => t.Id);
        builder.Entity<Tutor>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Tutor>().OwnsOne(t => t.TutorUserId, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.UserId).HasColumnName("UserId");
        });
        builder.Entity<Tutor>().OwnsOne(t => t.TutorSkills, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.Skills)
                .HasColumnName("Skills")
                .HasConversion(
                    skills => string.Join(",", skills),
                    value => value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .Metadata.SetValueComparer(new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<IList<string>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                ));
        });
        builder.Entity<Tutor>().Property(t => t.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Tutor>().Property(t => t.University).IsRequired().HasMaxLength(200);
        builder.Entity<Tutor>().Property(t => t.Career).IsRequired().HasMaxLength(200);
        builder.Entity<Tutor>().Property(t => t.Bio).IsRequired().HasMaxLength(1000);
        builder.Entity<Tutor>().Property(t => t.AvatarUrl).IsRequired().HasMaxLength(500);
        builder.Entity<Tutor>().Property(t => t.MainSubject).IsRequired().HasMaxLength(200);
        builder.Entity<Tutor>().Property(t => t.Rating).IsRequired();
        builder.Entity<Tutor>().Property(t => t.ReviewCount).IsRequired();
        builder.Entity<Tutor>().Property(t => t.Verified).IsRequired();
        builder.Entity<Tutor>().Property(t => t.Online).IsRequired();
        builder.Entity<Tutor>().Property(t => t.ExperienceYears).IsRequired();
    }
}
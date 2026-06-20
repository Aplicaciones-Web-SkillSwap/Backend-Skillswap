using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;

namespace SkillSwap.Platform.Workspace.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyWorkspaceConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Session>().ToTable("sessions");
        builder.Entity<Session>().HasKey(s => s.Id);
        builder.Entity<Session>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Session>().OwnsOne(s => s.SessionLearnerId, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.UserId).HasColumnName("LearnerId");
        });
        builder.Entity<Session>().OwnsOne(s => s.SessionTutorId, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.UserId).HasColumnName("TutorId");
        });
        builder.Entity<Session>().Property(s => s.Topic).IsRequired().HasMaxLength(200);
        builder.Entity<Session>().Property(s => s.Status).IsRequired().HasMaxLength(50);
        builder.Entity<Session>().Property(s => s.ScheduledAt).IsRequired();
        builder.Entity<Session>().Property(s => s.CourseId).IsRequired();

        builder.Entity<Message>().ToTable("messages");
        builder.Entity<Message>().HasKey(m => m.Id);
        builder.Entity<Message>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Message>().Property(m => m.SessionId).IsRequired();
        builder.Entity<Message>().Property(m => m.SenderId).IsRequired();
        builder.Entity<Message>().Property(m => m.Content).IsRequired().HasMaxLength(2000);
        builder.Entity<Message>().Property(m => m.FileUrl).HasMaxLength(500);
        builder.Entity<Message>().Property(m => m.FileName).HasMaxLength(200);
        builder.Entity<Message>().Property(m => m.QuizId).IsRequired(false);
        builder.Entity<Message>().Property(m => m.QuizTitle).HasMaxLength(200).IsRequired(false);
        builder.Entity<Message>().Property(m => m.SentAt).IsRequired();
    }
}
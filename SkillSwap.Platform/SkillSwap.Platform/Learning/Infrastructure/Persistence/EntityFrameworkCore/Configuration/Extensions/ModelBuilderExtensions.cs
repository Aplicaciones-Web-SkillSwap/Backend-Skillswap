using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Learning.Domain.Model.Aggregates;

namespace SkillSwap.Platform.Learning.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyLearningConfiguration(this ModelBuilder builder)
    {
  
        builder.Entity<Quiz>().ToTable("quizzes");
        builder.Entity<Quiz>().HasKey(q => q.Id);
        builder.Entity<Quiz>().Property(q => q.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Quiz>().Property(q => q.Title).IsRequired().HasMaxLength(200);
        builder.Entity<Quiz>().Property(q => q.Course).IsRequired().HasMaxLength(200);
        builder.Entity<Quiz>().Property(q => q.Description).HasMaxLength(1000);
        builder.Entity<Quiz>().OwnsOne(q => q.TutorId, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.UserId).HasColumnName("TutorId");
        });
        
        builder.Entity<Quiz>().OwnsMany(q => q.Questions, v =>
        {
            v.ToTable("quiz_questions");
            v.WithOwner().HasForeignKey("QuizId");
            v.Property<int>("Id").ValueGeneratedOnAdd();
            v.HasKey("Id");
            
            v.Property(p => p.QuestionString).IsRequired();
 
            v.Property(p => p.Answers).HasConversion(
                answers => string.Join("|", answers),
                value => value.Split("|", StringSplitOptions.RemoveEmptyEntries)
            );
            v.Property(p => p.CorrectAnswer).IsRequired();
        });

        builder.Entity<QuizAttempt>().ToTable("quiz_attempts");
        builder.Entity<QuizAttempt>().HasKey(a => a.Id);
        builder.Entity<QuizAttempt>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<QuizAttempt>().Property(a => a.QuizId).IsRequired();
        builder.Entity<QuizAttempt>().OwnsOne(a => a.AttemptLearnerId, v =>
        {
            v.WithOwner().HasForeignKey("Id");
            v.Property(p => p.UserId).HasColumnName("LearnerId");
        });
        builder.Entity<QuizAttempt>().Property(a => a.SessionId).IsRequired();
        builder.Entity<QuizAttempt>().Property(a => a.SelectedAnswers)
            .HasConversion(
                answers => string.Join(",", answers),
                value => value.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse).ToList()
            )
            .Metadata.SetValueComparer(new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<List<int>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            ));
        builder.Entity<QuizAttempt>().Property(a => a.Score).IsRequired();
        builder.Entity<QuizAttempt>().Property(a => a.Total).IsRequired();
        builder.Entity<QuizAttempt>().Property(a => a.CompletedAt).IsRequired();
    }
}
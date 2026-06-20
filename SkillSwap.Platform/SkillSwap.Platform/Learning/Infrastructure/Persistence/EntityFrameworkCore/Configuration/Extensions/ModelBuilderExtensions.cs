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
        builder.Entity<Quiz>().Property(q => q.TutorId).IsRequired();
        
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
    }
}
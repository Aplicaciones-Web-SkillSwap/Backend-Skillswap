using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class WipeOrphanedQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // The 2 quizzes left over from the pre-launch data wipe reference TutorIds (1, 3) that
            // don't correspond to any real tutor anymore. Confirmed explicitly with the project owner
            // before writing this migration; irreversible, so Down() is a no-op.
            migrationBuilder.Sql("DELETE FROM quiz_questions WHERE QuizId IN (1, 2);");
            migrationBuilder.Sql("DELETE FROM quizzes WHERE Id IN (1, 2);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Irreversible: deleted rows cannot be restored.
        }
    }
}

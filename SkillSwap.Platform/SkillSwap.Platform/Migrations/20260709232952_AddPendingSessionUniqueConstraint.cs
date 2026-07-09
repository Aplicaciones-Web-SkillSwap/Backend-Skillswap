using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddPendingSessionUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // A missing double-submit guard let two identical pending requests be created for the
            // same learner+tutor (session ids 7 and 8). Cancel the newer duplicate so the unique
            // constraint below can be added; the learner still has their original pending request.
            migrationBuilder.Sql("UPDATE sessions SET Status = 'cancelled' WHERE Id = 8 AND Status = 'pending';");

            // Generated column: only non-null while a session is pending, so a unique index on it
            // makes a second pending request between the same learner and tutor impossible at the
            // database level, closing the race condition in the application-layer check.
            migrationBuilder.Sql(@"
                ALTER TABLE sessions
                ADD COLUMN PendingKey VARCHAR(50) GENERATED ALWAYS AS (
                    CASE WHEN Status = 'pending' THEN CONCAT(LearnerId, '-', TutorId) ELSE NULL END
                ) STORED;
            ");
            migrationBuilder.Sql("ALTER TABLE sessions ADD UNIQUE INDEX UQ_sessions_PendingKey (PendingKey);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE sessions DROP INDEX UQ_sessions_PendingKey;");
            migrationBuilder.Sql("ALTER TABLE sessions DROP COLUMN PendingKey;");
            // The status='cancelled' backfill on session id 8 is not reversed: it's a data fix,
            // not a schema artifact.
        }
    }
}

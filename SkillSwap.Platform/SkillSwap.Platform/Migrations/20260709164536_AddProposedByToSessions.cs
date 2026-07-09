using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddProposedByToSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProposedByUserId",
                table: "sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Backfill: every existing session's current ScheduledAt was set by its learner
            // (there was no reschedule feature before this migration), so the learner is the proposer.
            migrationBuilder.Sql("UPDATE sessions SET ProposedByUserId = LearnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProposedByUserId",
                table: "sessions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewedLearnerIdToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewedLearnerId",
                table: "reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Backfill: every existing review was written by a learner about a tutor, so the
            // learner is always the session's learner.
            migrationBuilder.Sql(@"
                UPDATE reviews r
                JOIN sessions s ON r.SessionId = s.Id
                SET r.ReviewedLearnerId = s.LearnerId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewedLearnerId",
                table: "reviews");
        }
    }
}

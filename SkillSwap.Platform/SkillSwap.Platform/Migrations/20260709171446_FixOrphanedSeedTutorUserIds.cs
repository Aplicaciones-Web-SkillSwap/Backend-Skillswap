using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class FixOrphanedSeedTutorUserIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed tutor rows (ids 1-6) were inserted with placeholder UserIds (1, 10-14) before real
            // Iam accounts existed. Those values now collide with real users.Id auto-increment values,
            // causing a new signup to see a stranger's tutor bio/skills as their own on first profile visit.
            // Move them out of the real user-id range so they can never collide again.
            migrationBuilder.Sql("UPDATE tutors SET UserId = 900001 WHERE Id = 1 AND UserId = 1");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 900002 WHERE Id = 2 AND UserId = 10");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 900003 WHERE Id = 3 AND UserId = 11");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 900004 WHERE Id = 4 AND UserId = 12");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 900005 WHERE Id = 5 AND UserId = 13");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 900006 WHERE Id = 6 AND UserId = 14");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE tutors SET UserId = 1 WHERE Id = 1 AND UserId = 900001");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 10 WHERE Id = 2 AND UserId = 900002");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 11 WHERE Id = 3 AND UserId = 900003");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 12 WHERE Id = 4 AND UserId = 900004");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 13 WHERE Id = 5 AND UserId = 900005");
            migrationBuilder.Sql("UPDATE tutors SET UserId = 14 WHERE Id = 6 AND UserId = 900006");
        }
    }
}

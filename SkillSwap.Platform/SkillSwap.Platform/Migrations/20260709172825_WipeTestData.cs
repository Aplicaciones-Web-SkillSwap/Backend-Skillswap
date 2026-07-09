using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class WipeTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // One-time wipe of pre-launch test/demo data (all accounts, sessions, reviews, etc. created
            // while building the app) so the database starts empty for real usage. Confirmed explicitly
            // with the project owner before writing this migration; irreversible, so Down() is a no-op.
            migrationBuilder.Sql("SET FOREIGN_KEY_CHECKS = 0;");

            migrationBuilder.Sql("DELETE FROM quiz_attempts;");
            migrationBuilder.Sql("DELETE FROM messages;");
            migrationBuilder.Sql("DELETE FROM reviews;");
            migrationBuilder.Sql("DELETE FROM sanctions;");
            migrationBuilder.Sql("DELETE FROM reports;");
            migrationBuilder.Sql("DELETE FROM transactions;");
            migrationBuilder.Sql("DELETE FROM wallets;");
            migrationBuilder.Sql("DELETE FROM sessions;");
            migrationBuilder.Sql("DELETE FROM tutors;");
            migrationBuilder.Sql("DELETE FROM users;");

            migrationBuilder.Sql("ALTER TABLE quiz_attempts AUTO_INCREMENT = 1;");
            migrationBuilder.Sql("ALTER TABLE messages AUTO_INCREMENT = 1;");
            migrationBuilder.Sql("ALTER TABLE reviews AUTO_INCREMENT = 1;");
            migrationBuilder.Sql("ALTER TABLE sanctions AUTO_INCREMENT = 1;");
            migrationBuilder.Sql("ALTER TABLE reports AUTO_INCREMENT = 1;");
            migrationBuilder.Sql("ALTER TABLE transactions AUTO_INCREMENT = 1;");
            migrationBuilder.Sql("ALTER TABLE wallets AUTO_INCREMENT = 1;");
            migrationBuilder.Sql("ALTER TABLE sessions AUTO_INCREMENT = 1;");
            migrationBuilder.Sql("ALTER TABLE tutors AUTO_INCREMENT = 1;");
            migrationBuilder.Sql("ALTER TABLE users AUTO_INCREMENT = 1;");

            migrationBuilder.Sql("SET FOREIGN_KEY_CHECKS = 1;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Irreversible: deleted rows cannot be restored.
        }
    }
}

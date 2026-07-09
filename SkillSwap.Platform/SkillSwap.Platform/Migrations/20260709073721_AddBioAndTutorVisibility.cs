using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddBioAndTutorVisibility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "users",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "tutors",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "tutors");
        }
    }
}

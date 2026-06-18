using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddTutorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tutors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    University = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Career = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Bio = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    Skills = table.Column<string>(type: "longtext", nullable: false),
                    AvatarUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    MainSubject = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Rating = table.Column<double>(type: "double", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    Verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Online = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tutors", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tutors");
        }
    }
}

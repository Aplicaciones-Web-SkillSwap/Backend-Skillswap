using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtAndBanFieldsToSanctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcknowledgedAt",
                table: "sanctions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "sanctions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPermanent",
                table: "sanctions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            // Las sanciones existentes nunca tuvieron fecha de creación real:
            // se backfillean a hace un año para que jamás cuenten como "advertencias de este mes"
            // (evita un ban automático espurio al desplegar), y se marcan como ya reconocidas
            // para que nadie vea un popup de una sanción que ya conocía de antes.
            migrationBuilder.Sql(
                "UPDATE sanctions " +
                "SET CreatedAt = DATE_SUB(UTC_TIMESTAMP(), INTERVAL 1 YEAR), " +
                "    AcknowledgedAt = UTC_TIMESTAMP();");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcknowledgedAt",
                table: "sanctions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "sanctions");

            migrationBuilder.DropColumn(
                name: "IsPermanent",
                table: "sanctions");
        }
    }
}

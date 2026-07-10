using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwap.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountSentAndPlatformFeeToTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountSent",
                table: "transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PlatformFee",
                table: "transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            // Las filas existentes de tipo 'donation' solo tenían el monto neto (95%) en Amount;
            // se reconstruye el monto bruto original y la comisión a partir de esa relación fija.
            migrationBuilder.Sql(
                "UPDATE transactions " +
                "SET AmountSent = ROUND(Amount / 0.95, 2), " +
                "    PlatformFee = ROUND(Amount / 0.95, 2) - Amount " +
                "WHERE Type = 'donation';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountSent",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "PlatformFee",
                table: "transactions");
        }
    }
}

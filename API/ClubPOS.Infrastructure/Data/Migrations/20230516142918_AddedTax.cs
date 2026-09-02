using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RateName",
                table: "AccommodationRates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    TaxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.TaxId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRates_Tax1",
                table: "AccommodationRates",
                column: "Tax1");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRates_Tax2",
                table: "AccommodationRates",
                column: "Tax2");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRates_Tax3",
                table: "AccommodationRates",
                column: "Tax3");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRates_Tax4",
                table: "AccommodationRates",
                column: "Tax4");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRates_Tax5",
                table: "AccommodationRates",
                column: "Tax5");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax1",
                table: "AccommodationRates",
                column: "Tax1",
                principalTable: "Taxes",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax2",
                table: "AccommodationRates",
                column: "Tax2",
                principalTable: "Taxes",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax3",
                table: "AccommodationRates",
                column: "Tax3",
                principalTable: "Taxes",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax4",
                table: "AccommodationRates",
                column: "Tax4",
                principalTable: "Taxes",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax5",
                table: "AccommodationRates",
                column: "Tax5",
                principalTable: "Taxes",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax1",
                table: "AccommodationRates");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax2",
                table: "AccommodationRates");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax3",
                table: "AccommodationRates");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax4",
                table: "AccommodationRates");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationRates_Taxes_Tax5",
                table: "AccommodationRates");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropIndex(
                name: "IX_AccommodationRates_Tax1",
                table: "AccommodationRates");

            migrationBuilder.DropIndex(
                name: "IX_AccommodationRates_Tax2",
                table: "AccommodationRates");

            migrationBuilder.DropIndex(
                name: "IX_AccommodationRates_Tax3",
                table: "AccommodationRates");

            migrationBuilder.DropIndex(
                name: "IX_AccommodationRates_Tax4",
                table: "AccommodationRates");

            migrationBuilder.DropIndex(
                name: "IX_AccommodationRates_Tax5",
                table: "AccommodationRates");

            migrationBuilder.AlterColumn<string>(
                name: "RateName",
                table: "AccommodationRates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}

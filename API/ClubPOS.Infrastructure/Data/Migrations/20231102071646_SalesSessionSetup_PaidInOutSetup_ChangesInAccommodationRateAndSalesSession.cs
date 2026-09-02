using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SalesSessionSetup_PaidInOutSetup_ChangesInAccommodationRateAndSalesSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message1",
                table: "ReceiptMessagesAndCoupons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Disclaimer",
                table: "ReceiptMessagesAndCoupons",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesSessionId",
                table: "Problems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewMemberNameReceipt",
                table: "MembershipSales",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesSessionId",
                table: "MembershipSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EndDayOfWeek",
                table: "AccommodationRates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDayOfTheWeek",
                table: "AccommodationRates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalesSessions",
                columns: table => new
                {
                    SalesSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShiftNumber = table.Column<int>(type: "int", nullable: false),
                    BeginFloat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DrawerNumber = table.Column<int>(type: "int", nullable: false),
                    BusinessDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCurrentlyInUse = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesSessions", x => x.SalesSessionId);
                });

            migrationBuilder.CreateTable(
                name: "PaidInOuts",
                columns: table => new
                {
                    PaidInOutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesSessionId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidIn = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidInComment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PaidOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidOutComment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidInOuts", x => x.PaidInOutId);
                    table.ForeignKey(
                        name: "FK_PaidInOuts_SalesSessions_SalesSessionId",
                        column: x => x.SalesSessionId,
                        principalTable: "SalesSessions",
                        principalColumn: "SalesSessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Problems_SalesSessionId",
                table: "Problems",
                column: "SalesSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipSales_SalesSessionId",
                table: "MembershipSales",
                column: "SalesSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidInOuts_SalesSessionId",
                table: "PaidInOuts",
                column: "SalesSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipSales_SalesSessions_SalesSessionId",
                table: "MembershipSales",
                column: "SalesSessionId",
                principalTable: "SalesSessions",
                principalColumn: "SalesSessionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_SalesSessions_SalesSessionId",
                table: "Problems",
                column: "SalesSessionId",
                principalTable: "SalesSessions",
                principalColumn: "SalesSessionId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipSales_SalesSessions_SalesSessionId",
                table: "MembershipSales");

            migrationBuilder.DropForeignKey(
                name: "FK_Problems_SalesSessions_SalesSessionId",
                table: "Problems");

            migrationBuilder.DropTable(
                name: "PaidInOuts");

            migrationBuilder.DropTable(
                name: "SalesSessions");

            migrationBuilder.DropIndex(
                name: "IX_Problems_SalesSessionId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_MembershipSales_SalesSessionId",
                table: "MembershipSales");

            migrationBuilder.DropColumn(
                name: "SalesSessionId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "SalesSessionId",
                table: "MembershipSales");

            migrationBuilder.DropColumn(
                name: "EndDayOfWeek",
                table: "AccommodationRates");

            migrationBuilder.DropColumn(
                name: "StartDayOfTheWeek",
                table: "AccommodationRates");

            migrationBuilder.AlterColumn<string>(
                name: "Message1",
                table: "ReceiptMessagesAndCoupons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Disclaimer",
                table: "ReceiptMessagesAndCoupons",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "NewMemberNameReceipt",
                table: "MembershipSales",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}

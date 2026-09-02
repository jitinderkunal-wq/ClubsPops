using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DiscountRateSetup_ShiftSetup_ChangesInBookingRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscounts",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCheckedOut",
                table: "Bookings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountFactor",
                table: "BookingDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DiscountRateId",
                table: "BookingDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MemberDiscountDetail",
                table: "BookingDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NonMemberDiscount",
                table: "BookingDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceDiscount",
                table: "BookingDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "DiscountRates",
                columns: table => new
                {
                    DiscountRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationTypeId = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: true),
                    RateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Optional = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountFlatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_DiscountRates", x => x.DiscountRateId);
                    table.ForeignKey(
                        name: "FK_DiscountRates_AccommodationTypes_AccommodationTypeId",
                        column: x => x.AccommodationTypeId,
                        principalTable: "AccommodationTypes",
                        principalColumn: "AccommodationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShiftSetUps",
                columns: table => new
                {
                    ShiftSetupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftNumber = table.Column<int>(type: "int", nullable: false),
                    ShiftStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_ShiftSetUps", x => x.ShiftSetupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_DiscountRateId",
                table: "BookingDetails",
                column: "DiscountRateId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountRates_AccommodationTypeId",
                table: "DiscountRates",
                column: "AccommodationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_DiscountRates_DiscountRateId",
                table: "BookingDetails",
                column: "DiscountRateId",
                principalTable: "DiscountRates",
                principalColumn: "DiscountRateId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_DiscountRates_DiscountRateId",
                table: "BookingDetails");

            migrationBuilder.DropTable(
                name: "DiscountRates");

            migrationBuilder.DropTable(
                name: "ShiftSetUps");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_DiscountRateId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "TotalDiscounts",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DiscountFactor",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "DiscountRateId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "MemberDiscountDetail",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "NonMemberDiscount",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "PriceDiscount",
                table: "BookingDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCheckedOut",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_ReceipMessagesAndCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationStatusHistories_Accommodations_AccommodationId",
                table: "AccommodationStatusHistories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCheckedOut",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ReceiptMessagesAndCoupons",
                columns: table => new
                {
                    ReceiptMessagesAndCouponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disclaimer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Message1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Message2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CouponText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    GSTNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
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
                    table.PrimaryKey("PK_ReceiptMessagesAndCoupons", x => x.ReceiptMessagesAndCouponId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationStatusHistories_Accommodations_AccommodationId",
                table: "AccommodationStatusHistories",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "AccommodationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationStatusHistories_Accommodations_AccommodationId",
                table: "AccommodationStatusHistories");

            migrationBuilder.DropTable(
                name: "ReceiptMessagesAndCoupons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCheckedOut",
                table: "Bookings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationStatusHistories_Accommodations_AccommodationId",
                table: "AccommodationStatusHistories",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "AccommodationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

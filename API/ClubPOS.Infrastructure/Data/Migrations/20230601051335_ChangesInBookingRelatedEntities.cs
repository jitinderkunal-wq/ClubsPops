using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInBookingRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscounts",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalMemberDiscounts",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalOtherDiscounts",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalPromos",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalRateDiscounts",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CouponsDiscount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "MemberDiscount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "MembershipSales",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "OtherDiscountDollar",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "OtherDiscountReason",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PromoDiscount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RateDiscount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AccommodationOtherDisPercent",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "AccommodationOtherDiscount",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "DiscountFactor",
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

            migrationBuilder.AlterColumn<int>(
                name: "SalesTypeId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SalesTypeId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscounts",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMemberDiscounts",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalOtherDiscounts",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPromos",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRateDiscounts",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CouponsDiscount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MemberDiscount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MembershipSales",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OtherDiscountDollar",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "OtherDiscountReason",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PromoDiscount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RateDiscount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AccommodationOtherDisPercent",
                table: "BookingDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AccommodationOtherDiscount",
                table: "BookingDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "BookingDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountFactor",
                table: "BookingDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
        }
    }
}

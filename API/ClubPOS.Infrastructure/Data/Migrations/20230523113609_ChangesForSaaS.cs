using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesForSaaS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AccommodationStatusHistories",
                newName: "AccommodationStatusHistoryId");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Taxes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "SalesTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "BookingDetailStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "BookingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TypeName",
                table: "AccommodationTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AccommodationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AccommodationSubItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AccommodationStatusHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AccommodationStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Accommodations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AccommodationRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AccommodationItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "SalesTypes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "BookingDetailStatuses");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AccommodationTypes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AccommodationSubItems");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AccommodationStatusHistories");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AccommodationStatuses");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AccommodationRates");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AccommodationItems");

            migrationBuilder.RenameColumn(
                name: "AccommodationStatusHistoryId",
                table: "AccommodationStatusHistories",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "TypeName",
                table: "AccommodationTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedExtraFildsToEachEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Transactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Taxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Taxes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Taxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Taxes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "Taxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "Taxes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Taxes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SalesTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SalesTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "SalesTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "SalesTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "SalesTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "SalesTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SalesTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bookings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BookingDetailStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BookingDetailStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookingDetailStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "BookingDetailStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "BookingDetailStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "BookingDetailStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookingDetailStatuses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BookingDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BookingDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookingDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "BookingDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "BookingDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "BookingDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookingDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AccommodationTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AccommodationTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AccommodationTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AccommodationTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "AccommodationTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "AccommodationTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AccommodationTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AccommodationStatusHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AccommodationStatusHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AccommodationStatusHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AccommodationStatusHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "AccommodationStatusHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "AccommodationStatusHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AccommodationStatusHistories",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AccommodationStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AccommodationStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AccommodationStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AccommodationStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "AccommodationStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "AccommodationStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AccommodationStatuses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Accommodations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Accommodations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "Accommodations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Accommodations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AccommodationRates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AccommodationRates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AccommodationRates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AccommodationRates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "AccommodationRates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "AccommodationRates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AccommodationRates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AccommodationItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AccommodationItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AccommodationItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AccommodationItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "AccommodationItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "AccommodationItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AccommodationItems",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SalesTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SalesTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "SalesTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "SalesTypes");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "SalesTypes");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "SalesTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SalesTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BookingDetailStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BookingDetailStatuses");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookingDetailStatuses");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "BookingDetailStatuses");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "BookingDetailStatuses");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "BookingDetailStatuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookingDetailStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AccommodationTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AccommodationTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AccommodationTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AccommodationTypes");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "AccommodationTypes");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "AccommodationTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AccommodationTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AccommodationStatusHistories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AccommodationStatusHistories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AccommodationStatusHistories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AccommodationStatusHistories");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "AccommodationStatusHistories");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "AccommodationStatusHistories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AccommodationStatusHistories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AccommodationStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AccommodationStatuses");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AccommodationStatuses");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AccommodationStatuses");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "AccommodationStatuses");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "AccommodationStatuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AccommodationStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AccommodationRates");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AccommodationRates");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AccommodationRates");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AccommodationRates");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "AccommodationRates");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "AccommodationRates");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AccommodationRates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AccommodationItems");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AccommodationItems");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AccommodationItems");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AccommodationItems");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "AccommodationItems");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "AccommodationItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AccommodationItems");
        }
    }
}

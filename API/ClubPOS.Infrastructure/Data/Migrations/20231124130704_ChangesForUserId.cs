using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesForUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MaintenanceLogs");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "MaintenanceLogs");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "CleaningLogs");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "WaitingLists",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "WaitingLists",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "WaitingLists",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Transactions",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Transactions",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Transactions",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Taxes",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Taxes",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Taxes",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "ShiftSetUps",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "ShiftSetUps",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ShiftSetUps",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "SalesTypes",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SalesTypes",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "SalesTypes",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "SalesSessions",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SalesSessions",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "SalesSessions",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "ReceiptMessagesAndCoupons",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "ReceiptMessagesAndCoupons",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ReceiptMessagesAndCoupons",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EmployeeName",
                table: "Problems",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Problems",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Problems",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "ProblemCategories",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "ProblemCategories",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ProblemCategories",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "PaidInOuts",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "PaidInOuts",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "PaidInOuts",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "MembershipTypes",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "MembershipTypes",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "MembershipTypes",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "MembershipSales",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "MembershipSales",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "MembershipSales",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Memberships",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Memberships",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Memberships",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "MaintenanceTasks",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "MaintenanceTasks",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "MaintenanceTasks",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EmployeeResolved",
                table: "MaintenanceLogs",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "MaintenanceLogs",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "MaintenanceLogs",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "MaintenanceLogDetails",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "MaintenanceLogDetails",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "MaintenanceLogDetails",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "ExchangeRates",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "ExchangeRates",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ExchangeRates",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "DrawerSkims",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "DrawerSkims",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "DrawerSkims",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "DiscountRates",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "DiscountRates",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "DiscountRates",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "CreditCardTypes",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "CreditCardTypes",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CreditCardTypes",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Coupons",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Coupons",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Coupons",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "CouponDetails",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "CouponDetails",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CouponDetails",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "CleaningTasks",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "CleaningTasks",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CleaningTasks",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "CleaningLogs",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "CleaningLogs",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CleaningLogs",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "CleaningLogDetails",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "CleaningLogDetails",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CleaningLogDetails",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "CashierReconciliations",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "CashierReconciliations",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CashierReconciliations",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Bookings",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Bookings",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Bookings",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "BookingDetailStatuses",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "BookingDetailStatuses",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "BookingDetailStatuses",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "BookingDetails",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "BookingDetails",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "BookingDetails",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "AccommodationTypes",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AccommodationTypes",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "AccommodationTypes",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "AccommodationStatusHistories",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AccommodationStatusHistories",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "AccommodationStatusHistories",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "AccommodationStatuses",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AccommodationStatuses",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "AccommodationStatuses",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Accommodations",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Accommodations",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Accommodations",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "AccommodationRates",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AccommodationRates",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "AccommodationRates",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "AccommodationItems",
                newName: "EditedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AccommodationItems",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "AccommodationItems",
                newName: "CreatedByUserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WaitingLists",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Transactions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "SalesSessions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Problems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResolvedUserId",
                table: "MaintenanceLogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MaintenanceLogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CleaningLogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "ResolvedUserId",
                table: "MaintenanceLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MaintenanceLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CleaningLogs");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "WaitingLists",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "WaitingLists",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "WaitingLists",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "Transactions",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Transactions",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Transactions",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "Taxes",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Taxes",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Taxes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "ShiftSetUps",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "ShiftSetUps",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "ShiftSetUps",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "SalesTypes",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "SalesTypes",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "SalesTypes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "SalesSessions",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "SalesSessions",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "SalesSessions",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "ReceiptMessagesAndCoupons",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "ReceiptMessagesAndCoupons",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "ReceiptMessagesAndCoupons",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "Problems",
                newName: "EmployeeName");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Problems",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Problems",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "ProblemCategories",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "ProblemCategories",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "ProblemCategories",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "PaidInOuts",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "PaidInOuts",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "PaidInOuts",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "MembershipTypes",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "MembershipTypes",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "MembershipTypes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "MembershipSales",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "MembershipSales",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "MembershipSales",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "Memberships",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Memberships",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Memberships",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "MaintenanceTasks",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "MaintenanceTasks",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "MaintenanceTasks",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "MaintenanceLogs",
                newName: "EmployeeResolved");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "MaintenanceLogs",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "MaintenanceLogs",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "MaintenanceLogDetails",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "MaintenanceLogDetails",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "MaintenanceLogDetails",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "ExchangeRates",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "ExchangeRates",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "ExchangeRates",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "DrawerSkims",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "DrawerSkims",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "DrawerSkims",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "DiscountRates",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "DiscountRates",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "DiscountRates",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "CreditCardTypes",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "CreditCardTypes",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "CreditCardTypes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "Coupons",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Coupons",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Coupons",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "CouponDetails",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "CouponDetails",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "CouponDetails",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "CleaningTasks",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "CleaningTasks",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "CleaningTasks",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "CleaningLogs",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "CleaningLogs",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "CleaningLogs",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "CleaningLogDetails",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "CleaningLogDetails",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "CleaningLogDetails",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "CashierReconciliations",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "CashierReconciliations",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "CashierReconciliations",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "Bookings",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Bookings",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Bookings",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "BookingDetailStatuses",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "BookingDetailStatuses",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "BookingDetailStatuses",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "BookingDetails",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "BookingDetails",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "BookingDetails",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "AccommodationTypes",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "AccommodationTypes",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "AccommodationTypes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "AccommodationStatusHistories",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "AccommodationStatusHistories",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "AccommodationStatusHistories",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "AccommodationStatuses",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "AccommodationStatuses",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "AccommodationStatuses",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "Accommodations",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Accommodations",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Accommodations",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "AccommodationRates",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "AccommodationRates",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "AccommodationRates",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedByUserId",
                table: "AccommodationItems",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "AccommodationItems",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "AccommodationItems",
                newName: "CreatedBy");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "WaitingLists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Transactions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "SalesSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MaintenanceLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "MaintenanceLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "CleaningLogs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}

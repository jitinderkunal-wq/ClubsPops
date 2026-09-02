using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_CleaningLog_Coupon_CreditCard_ExchangeRate_MembershipAndWaitingList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Transactions_TransactionId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "MaintenanceLogs");

            migrationBuilder.RenameColumn(
                name: "UsExchangeReceived",
                table: "Transactions",
                newName: "USExchangeReceived");

            migrationBuilder.RenameColumn(
                name: "UsExchangePaid",
                table: "Transactions",
                newName: "USExchangePaid");

            migrationBuilder.RenameColumn(
                name: "TotalUsdollars",
                table: "Transactions",
                newName: "TotalUSDollars");

            migrationBuilder.RenameColumn(
                name: "Usdollars",
                table: "Bookings",
                newName: "USDollars");

            migrationBuilder.RenameColumn(
                name: "UsExchangeRate",
                table: "Bookings",
                newName: "USExchangeRate");

            migrationBuilder.RenameColumn(
                name: "OtherCreditCard3",
                table: "Bookings",
                newName: "UnionPay");

            migrationBuilder.RenameColumn(
                name: "OtherCreditCard2",
                table: "Bookings",
                newName: "TotalDiscount");

            migrationBuilder.RenameColumn(
                name: "OtherCreditCard1",
                table: "Bookings",
                newName: "RateDiscount");

            migrationBuilder.RenameColumn(
                name: "Discover",
                table: "Bookings",
                newName: "PromoDiscount");

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMemberDiscounts",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMembershipSales",
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

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Problems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MembershipSaleId",
                table: "Problems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "MaintenanceLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CouponsDiscount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Diners",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscoveryCard",
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
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "BookingDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromopassId",
                table: "BookingDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Hours",
                table: "AccommodationRates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CleaningLogs",
                columns: table => new
                {
                    CleaningLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NeedsMaintenance = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_CleaningLogs", x => x.CleaningLogId);
                    table.ForeignKey(
                        name: "FK_CleaningLogs_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CleaningTasks",
                columns: table => new
                {
                    CleaningTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_CleaningTasks", x => x.CleaningTaskId);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationTypeId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CouponName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CouponNumber = table.Column<int>(type: "int", nullable: false),
                    FlatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountDollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsExclusive = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsCoupon = table.Column<bool>(type: "bit", nullable: false),
                    ReturnOnCheckOut = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                    table.ForeignKey(
                        name: "FK_Coupons_AccommodationTypes_AccommodationTypeId",
                        column: x => x.AccommodationTypeId,
                        principalTable: "AccommodationTypes",
                        principalColumn: "AccommodationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditCardTypes",
                columns: table => new
                {
                    CreditCardTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditCardTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
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
                    table.PrimaryKey("PK_CreditCardTypes", x => x.CreditCardTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    ExchangeRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RateName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_ExchangeRates", x => x.ExchangeRateId);
                });

            migrationBuilder.CreateTable(
                name: "MembershipTypes",
                columns: table => new
                {
                    MembershipTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembershipTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DiscountFactor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositRequired = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CanReserve = table.Column<bool>(type: "bit", nullable: false),
                    CanTab = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MemberShipPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_MembershipTypes", x => x.MembershipTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CleaningLogDetails",
                columns: table => new
                {
                    CleaningLogDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CleaningLogId = table.Column<int>(type: "int", nullable: false),
                    CleaningTaskId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CleaningLogDetails", x => x.CleaningLogDetailId);
                    table.ForeignKey(
                        name: "FK_CleaningLogDetails_CleaningLogs_CleaningLogId",
                        column: x => x.CleaningLogId,
                        principalTable: "CleaningLogs",
                        principalColumn: "CleaningLogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CleaningLogDetails_CleaningTasks_CleaningTaskId",
                        column: x => x.CleaningTaskId,
                        principalTable: "CleaningTasks",
                        principalColumn: "CleaningTaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CouponDetails",
                columns: table => new
                {
                    CouponDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponId = table.Column<int>(type: "int", nullable: false),
                    AccommodationRateId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CouponDetails", x => x.CouponDetailId);
                    table.ForeignKey(
                        name: "FK_CouponDetails_AccommodationRates_AccommodationRateId",
                        column: x => x.AccommodationRateId,
                        principalTable: "AccommodationRates",
                        principalColumn: "AccommodationRateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CouponDetails_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "CouponId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    MembershipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditCardTypeId = table.Column<int>(type: "int", nullable: true),
                    MembershipTypeId = table.Column<int>(type: "int", nullable: false),
                    MembershipNum = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditCardExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MembershipStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MembershipEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PostalZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Csv = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Memberships", x => x.MembershipId);
                    table.ForeignKey(
                        name: "FK_Memberships_CreditCardTypes_CreditCardTypeId",
                        column: x => x.CreditCardTypeId,
                        principalTable: "CreditCardTypes",
                        principalColumn: "CreditCardTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Memberships_MembershipTypes_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "MembershipTypes",
                        principalColumn: "MembershipTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MembershipSales",
                columns: table => new
                {
                    MembershipSalesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembershipTypeId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    MembershipPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MembershipSaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrontDeskSale = table.Column<bool>(type: "bit", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tendered = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Visa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MasterCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCreditCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    USExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    USDollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EuroExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EuroDollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewMemberNameReceipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MembershipSales", x => x.MembershipSalesId);
                    table.ForeignKey(
                        name: "FK_MembershipSales_MembershipTypes_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "MembershipTypes",
                        principalColumn: "MembershipTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembershipSales_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaitingLists",
                columns: table => new
                {
                    WaitingListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    AccommodationTypeId = table.Column<int>(type: "int", nullable: true),
                    MembershipId = table.Column<int>(type: "int", nullable: true),
                    MembershipTypeId = table.Column<int>(type: "int", nullable: false),
                    RoomAssignedId = table.Column<int>(type: "int", nullable: true),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WaitListTimeAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_WaitingLists", x => x.WaitingListId);
                    table.ForeignKey(
                        name: "FK_WaitingLists_AccommodationTypes_AccommodationTypeId",
                        column: x => x.AccommodationTypeId,
                        principalTable: "AccommodationTypes",
                        principalColumn: "AccommodationTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WaitingLists_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaitingLists_Accommodations_RoomAssignedId",
                        column: x => x.RoomAssignedId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId");
                    table.ForeignKey(
                        name: "FK_WaitingLists_MembershipTypes_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "MembershipTypes",
                        principalColumn: "MembershipTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaitingLists_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "MembershipId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WaitingLists_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MembershipId",
                table: "Transactions",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_MembershipId",
                table: "Problems",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_MembershipSaleId",
                table: "Problems",
                column: "MembershipSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_CouponId",
                table: "BookingDetails",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_MembershipId",
                table: "BookingDetails",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_PromopassId",
                table: "BookingDetails",
                column: "PromopassId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningLogDetails_CleaningLogId",
                table: "CleaningLogDetails",
                column: "CleaningLogId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningLogDetails_CleaningTaskId",
                table: "CleaningLogDetails",
                column: "CleaningTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningLogs_AccommodationId",
                table: "CleaningLogs",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponDetails_AccommodationRateId",
                table: "CouponDetails",
                column: "AccommodationRateId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponDetails_CouponId",
                table: "CouponDetails",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_AccommodationTypeId",
                table: "Coupons",
                column: "AccommodationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_CreditCardTypeId",
                table: "Memberships",
                column: "CreditCardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_MembershipTypeId",
                table: "Memberships",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipSales_MembershipTypeId",
                table: "MembershipSales",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipSales_TransactionId",
                table: "MembershipSales",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingLists_AccommodationId",
                table: "WaitingLists",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingLists_AccommodationTypeId",
                table: "WaitingLists",
                column: "AccommodationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingLists_MembershipId",
                table: "WaitingLists",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingLists_MembershipTypeId",
                table: "WaitingLists",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingLists_RoomAssignedId",
                table: "WaitingLists",
                column: "RoomAssignedId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingLists_TransactionId",
                table: "WaitingLists",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Coupons_CouponId",
                table: "BookingDetails",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "CouponId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Coupons_PromopassId",
                table: "BookingDetails",
                column: "PromopassId",
                principalTable: "Coupons",
                principalColumn: "CouponId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Memberships_MembershipId",
                table: "BookingDetails",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "MembershipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Transactions_TransactionId",
                table: "Bookings",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_MembershipSales_MembershipSaleId",
                table: "Problems",
                column: "MembershipSaleId",
                principalTable: "MembershipSales",
                principalColumn: "MembershipSalesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_Memberships_MembershipId",
                table: "Problems",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "MembershipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Memberships_MembershipId",
                table: "Transactions",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "MembershipId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Coupons_CouponId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Coupons_PromopassId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Memberships_MembershipId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Transactions_TransactionId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Problems_MembershipSales_MembershipSaleId",
                table: "Problems");

            migrationBuilder.DropForeignKey(
                name: "FK_Problems_Memberships_MembershipId",
                table: "Problems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Memberships_MembershipId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "CleaningLogDetails");

            migrationBuilder.DropTable(
                name: "CouponDetails");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "MembershipSales");

            migrationBuilder.DropTable(
                name: "WaitingLists");

            migrationBuilder.DropTable(
                name: "CleaningLogs");

            migrationBuilder.DropTable(
                name: "CleaningTasks");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "CreditCardTypes");

            migrationBuilder.DropTable(
                name: "MembershipTypes");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_MembershipId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Problems_MembershipId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_MembershipSaleId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_CouponId",
                table: "BookingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_MembershipId",
                table: "BookingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_PromopassId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalMemberDiscounts",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalMembershipSales",
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
                name: "MembershipId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "MembershipSaleId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "MaintenanceLogs");

            migrationBuilder.DropColumn(
                name: "CouponsDiscount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Diners",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DiscoveryCard",
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
                name: "AccommodationOtherDisPercent",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "AccommodationOtherDiscount",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "PromopassId",
                table: "BookingDetails");

            migrationBuilder.RenameColumn(
                name: "USExchangeReceived",
                table: "Transactions",
                newName: "UsExchangeReceived");

            migrationBuilder.RenameColumn(
                name: "USExchangePaid",
                table: "Transactions",
                newName: "UsExchangePaid");

            migrationBuilder.RenameColumn(
                name: "TotalUSDollars",
                table: "Transactions",
                newName: "TotalUsdollars");

            migrationBuilder.RenameColumn(
                name: "USExchangeRate",
                table: "Bookings",
                newName: "UsExchangeRate");

            migrationBuilder.RenameColumn(
                name: "USDollars",
                table: "Bookings",
                newName: "Usdollars");

            migrationBuilder.RenameColumn(
                name: "UnionPay",
                table: "Bookings",
                newName: "OtherCreditCard3");

            migrationBuilder.RenameColumn(
                name: "TotalDiscount",
                table: "Bookings",
                newName: "OtherCreditCard2");

            migrationBuilder.RenameColumn(
                name: "RateDiscount",
                table: "Bookings",
                newName: "OtherCreditCard1");

            migrationBuilder.RenameColumn(
                name: "PromoDiscount",
                table: "Bookings",
                newName: "Discover");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "MaintenanceLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Hours",
                table: "AccommodationRates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Transactions_TransactionId",
                table: "Bookings",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

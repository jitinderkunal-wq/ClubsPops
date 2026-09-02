using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookingSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingDetailStatuses",
                columns: table => new
                {
                    BookingDetailStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDetailStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetailStatuses", x => x.BookingDetailStatusId);
                });

            migrationBuilder.CreateTable(
                name: "SalesTypes",
                columns: table => new
                {
                    SalesTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTypes", x => x.SalesTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesTypeId = table.Column<int>(type: "int", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumOfGuests = table.Column<int>(type: "int", nullable: false),
                    TransactionStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionCheckInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransacationCheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDeposits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalReturnedDeposits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalNonReturnedDeposits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCoupons = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRefunds = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountTendered = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTransfer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVoids = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCancelled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StoredOrder = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StoredOrderTypeId = table.Column<int>(type: "int", nullable: true),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MasterCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Visa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscoveryCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnionPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Diners = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalUsdollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsExchangePaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsExchangeReceived = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionTimeCheckedOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalMemberDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPromos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOtherDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_SalesTypes_SalesTypeId",
                        column: x => x.SalesTypeId,
                        principalTable: "SalesTypes",
                        principalColumn: "SalesTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDeposits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Refunded = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tendered = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MasterCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Visa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCreditCard1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCreditCard2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCreditCard3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RateDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDiscountDollar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherDiscountReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeCheckedOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WakeUpCall = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonRefundedDeposits = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RefundComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RateTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemberDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CouponsDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromoDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeCompleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckedOutByEmployee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewCreditCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewCardExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShiftBookedIn = table.Column<int>(type: "int", nullable: false),
                    ShiftBookedOut = table.Column<int>(type: "int", nullable: false),
                    ReceiptMessage1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptMessage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptCouponText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disclaimer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Usdollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EuroDollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EuroExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MembershipSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CheckInLoginId = table.Column<int>(type: "int", nullable: true),
                    CheckOutLoginId = table.Column<int>(type: "int", nullable: true),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    AccommodationItemId = table.Column<int>(type: "int", nullable: true),
                    AccommodationRateId = table.Column<int>(type: "int", nullable: true),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    BookingDetailStatusId = table.Column<int>(type: "int", nullable: true),
                    HoursOfStay = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HasDeposit = table.Column<bool>(type: "bit", nullable: false),
                    DiscountFactor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonMemberDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Returned = table.Column<int>(type: "int", nullable: false),
                    Transfered = table.Column<int>(type: "int", nullable: false),
                    MemberDiscountDetail = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccommodationCouponDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccommodationPromopassDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: true),
                    TransferDepositDetail = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferDepositsReturned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccommodationOtherDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccommodationOtherDisPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CheckOutTimeRequested = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChargableDeposit = table.Column<bool>(type: "bit", nullable: false),
                    DepositTransRefund = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsKeyRefundedOnly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.BookingDetailId);
                    table.ForeignKey(
                        name: "FK_BookingDetails_AccommodationItems_AccommodationItemId",
                        column: x => x.AccommodationItemId,
                        principalTable: "AccommodationItems",
                        principalColumn: "AccommodationItemId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BookingDetails_AccommodationRates_AccommodationRateId",
                        column: x => x.AccommodationRateId,
                        principalTable: "AccommodationRates",
                        principalColumn: "AccommodationRateId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_BookingDetailStatuses_BookingDetailStatusId",
                        column: x => x.BookingDetailStatusId,
                        principalTable: "BookingDetailStatuses",
                        principalColumn: "BookingDetailStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_AccommodationId",
                table: "BookingDetails",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_AccommodationItemId",
                table: "BookingDetails",
                column: "AccommodationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_AccommodationRateId",
                table: "BookingDetails",
                column: "AccommodationRateId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingDetailStatusId",
                table: "BookingDetails",
                column: "BookingDetailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TransactionId",
                table: "Bookings",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SalesTypeId",
                table: "Transactions",
                column: "SalesTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "BookingDetailStatuses");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "SalesTypes");
        }
    }
}

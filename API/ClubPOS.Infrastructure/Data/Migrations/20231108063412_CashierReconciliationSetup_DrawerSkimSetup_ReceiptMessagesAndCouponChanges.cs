using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CashierReconciliationSetup_DrawerSkimSetup_ReceiptMessagesAndCouponChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashierReconciliations",
                columns: table => new
                {
                    CashierReconciliationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesSessionId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccommodationSalesPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoidedAccommodationsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateDiscountsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CouponDiscountsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromodiscouuntsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherDiscountsRolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemberDiscountsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAccommodationSalesPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccommodationSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoidedAccommodations = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CouponDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Promodiscouunts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemberDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAccommodationSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositsCollectedPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositsReturnedPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MembershipSalesPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherSalesPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BarSalesPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalSalesTotalPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositsCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositsReturned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MembershipSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BarSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalSalesTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MasterCardPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Visapolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amexpolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscoverPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCreditCard1Polled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDebitCreditPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MasterCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Visa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCreditCard1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDebitCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsExchangeRatePolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EuroExchangeRatePolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalUsdollarsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEuroDollarsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EuroExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalUsdollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEuroDollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidInsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalCashDepositPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptsPaidOutsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCashDropsPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashOverShortPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalCashDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidIns = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptsPaidOuts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCashDrops = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashOverShort = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop1Polled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop2Polled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop3Polled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop4Polled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop5Polled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop6Polled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDrop6 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionCount = table.Column<int>(type: "int", nullable: false),
                    AccommodationSalesCount = table.Column<int>(type: "int", nullable: false),
                    VoidedAccommodationsCount = table.Column<int>(type: "int", nullable: false),
                    RateDiscountsCount = table.Column<int>(type: "int", nullable: false),
                    CouponDiscountsCount = table.Column<int>(type: "int", nullable: false),
                    PromoDiscouuntsCount = table.Column<int>(type: "int", nullable: false),
                    OtherDiscountsCount = table.Column<int>(type: "int", nullable: false),
                    MemberDiscountsCount = table.Column<int>(type: "int", nullable: false),
                    DepositsCollectedCount = table.Column<int>(type: "int", nullable: false),
                    DepositsReturnedCount = table.Column<int>(type: "int", nullable: false),
                    MembershipSalesCount = table.Column<int>(type: "int", nullable: false),
                    OtherSalesCount = table.Column<int>(type: "int", nullable: false),
                    BarSalesCount = table.Column<int>(type: "int", nullable: false),
                    FinalSalesTotalCount = table.Column<int>(type: "int", nullable: false),
                    DebitCount = table.Column<int>(type: "int", nullable: false),
                    MasterCardCount = table.Column<int>(type: "int", nullable: false),
                    VisaCount = table.Column<int>(type: "int", nullable: false),
                    AmexCount = table.Column<int>(type: "int", nullable: false),
                    DiscoverCount = table.Column<int>(type: "int", nullable: false),
                    OtherCreditCard1Count = table.Column<int>(type: "int", nullable: false),
                    TotalDebitCreditCount = table.Column<int>(type: "int", nullable: false),
                    TotalUsdollarsCount = table.Column<int>(type: "int", nullable: false),
                    TotalEuroDollarsCount = table.Column<int>(type: "int", nullable: false),
                    CashCount = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalCashDepositCount = table.Column<int>(type: "int", nullable: false),
                    PaidInsCount = table.Column<int>(type: "int", nullable: false),
                    ReceiptsPaidOutsCount = table.Column<int>(type: "int", nullable: false),
                    IsReconciled = table.Column<bool>(type: "bit", nullable: false),
                    EosNickels = table.Column<int>(type: "int", nullable: false),
                    EosDimes = table.Column<int>(type: "int", nullable: false),
                    EosQuarters = table.Column<int>(type: "int", nullable: false),
                    EosLoonies = table.Column<int>(type: "int", nullable: false),
                    EosTwonies = table.Column<int>(type: "int", nullable: false),
                    EosFives = table.Column<int>(type: "int", nullable: false),
                    EosTens = table.Column<int>(type: "int", nullable: false),
                    EosTwentys = table.Column<int>(type: "int", nullable: false),
                    EosFiftys = table.Column<int>(type: "int", nullable: false),
                    EosOneHundreds = table.Column<int>(type: "int", nullable: false),
                    EosDepositTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosVisa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosMasterCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosAmex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosDiscover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosOtherCreditCard1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosUsDollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosEuroDollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosCheques = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosFinalTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosQuickCashDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosQuickDebitCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosDebitCreditTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalesEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EosUsDollarsConverted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EosEuroDollarsConverted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashDropCount = table.Column<int>(type: "int", nullable: false),
                    RefundsTotalPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RefundsTotalCountPolled = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RefundsTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_CashierReconciliations", x => x.CashierReconciliationId);
                    table.ForeignKey(
                        name: "FK_CashierReconciliations_SalesSessions_SalesSessionId",
                        column: x => x.SalesSessionId,
                        principalTable: "SalesSessions",
                        principalColumn: "SalesSessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DrawerSkims",
                columns: table => new
                {
                    DrawerSkimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesSessionId = table.Column<int>(type: "int", nullable: false),
                    SkimDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SkimShiftCount = table.Column<int>(type: "int", nullable: true),
                    TotalCoin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Qty1Dollar = table.Column<int>(type: "int", nullable: true),
                    Qty2Dollar = table.Column<int>(type: "int", nullable: true),
                    Qty5Dollar = table.Column<int>(type: "int", nullable: true),
                    Qty10Dollar = table.Column<int>(type: "int", nullable: true),
                    Qty20Dollar = table.Column<int>(type: "int", nullable: true),
                    Qty50Dollar = table.Column<int>(type: "int", nullable: true),
                    Qty100Dollar = table.Column<int>(type: "int", nullable: true),
                    TotalCheques = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalOther = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalEuros = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Qty1DollarUs = table.Column<int>(type: "int", nullable: true),
                    Qty2DollarUs = table.Column<int>(type: "int", nullable: true),
                    Qty5DollarUs = table.Column<int>(type: "int", nullable: true),
                    Qty10DollarUs = table.Column<int>(type: "int", nullable: true),
                    Qty20DollarUs = table.Column<int>(type: "int", nullable: true),
                    Qty50DollarUs = table.Column<int>(type: "int", nullable: true),
                    Qty100DollarUs = table.Column<int>(type: "int", nullable: true),
                    CoinTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total1Dollar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total2Dollar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total5Dollar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total10Dollar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total20Dollar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total50Dollar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total100Dollar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total1DollarUs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total2DollarUs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total5DollarUs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total10DollarUs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total20DollarUs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total50DollarUs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total100DollarUs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChequeTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EuroTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalUsdollars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCaddollar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_DrawerSkims", x => x.DrawerSkimId);
                    table.ForeignKey(
                        name: "FK_DrawerSkims_SalesSessions_SalesSessionId",
                        column: x => x.SalesSessionId,
                        principalTable: "SalesSessions",
                        principalColumn: "SalesSessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashierReconciliations_SalesSessionId",
                table: "CashierReconciliations",
                column: "SalesSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawerSkims_SalesSessionId",
                table: "DrawerSkims",
                column: "SalesSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashierReconciliations");

            migrationBuilder.DropTable(
                name: "DrawerSkims");
        }
    }
}

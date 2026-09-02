using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class CashierReconciliationConfiguration : IEntityTypeConfiguration<CashierReconciliation>
{
    public void Configure(EntityTypeBuilder<CashierReconciliation> builder)
    {
        builder.HasKey(cr => cr.Id);

        // Configure properties
        builder.Property(cr => cr.Id).HasColumnName("CashierReconciliationId").IsRequired();
        builder.Property(cr => cr.SalesSessionId).IsRequired();
        builder.Property(cr => cr.DateCreated).IsRequired();
        builder.Property(cr => cr.AccommodationSalesPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.VoidedAccommodationsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.RateDiscountsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CouponDiscountsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.PromodiscouuntsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.OtherDiscountsRolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.MemberDiscountsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalAccommodationSalesPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.AccommodationSales).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.VoidedAccommodations).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.RateDiscounts).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CouponDiscounts).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.Promodiscouunts).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.OtherDiscounts).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.MemberDiscounts).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalAccommodationSales).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.DepositsCollectedPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.DepositsReturnedPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.MembershipSalesPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.OtherSalesPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.BarSalesPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.FinalSalesTotalPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.DepositsCollected).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.DepositsReturned).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.MembershipSales).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.OtherSales).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.BarSales).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.FinalSalesTotal).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.DebitPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.MasterCardPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.Visapolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.Amexpolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.DiscoverPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.OtherCreditCard1Polled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalDebitCreditPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.Debit).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.MasterCard).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.Visa).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.Amex).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.Discover).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.OtherCreditCard1).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalDebitCredit).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.UsExchangeRatePolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EuroExchangeRatePolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalUsdollarsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalEuroDollarsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.UsExchangeRate).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EuroExchangeRate).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalUsdollars).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalEuroDollars).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.PaidInsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.FinalCashDepositPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.ReceiptsPaidOutsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalCashDropsPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashOverShortPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.FinalCashDeposit).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.PaidIns).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.ReceiptsPaidOuts).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TotalCashDrops).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashOverShort).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop1Polled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop1).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop2Polled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop2).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop3Polled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop3).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop4Polled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop4).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop5Polled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop5).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop6Polled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDrop6).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.TransactionCount).IsRequired();
        builder.Property(cr => cr.AccommodationSalesCount).IsRequired();
        builder.Property(cr => cr.VoidedAccommodationsCount).IsRequired();
        builder.Property(cr => cr.RateDiscountsCount).IsRequired();
        builder.Property(cr => cr.CouponDiscountsCount).IsRequired();
        builder.Property(cr => cr.PromoDiscouuntsCount).IsRequired();
        builder.Property(cr => cr.OtherDiscountsCount).IsRequired();
        builder.Property(cr => cr.MemberDiscountsCount).IsRequired();
        builder.Property(cr => cr.DepositsCollectedCount).IsRequired();
        builder.Property(cr => cr.DepositsReturnedCount).IsRequired();
        builder.Property(cr => cr.MembershipSalesCount).IsRequired();
        builder.Property(cr => cr.OtherSalesCount).IsRequired();
        builder.Property(cr => cr.BarSalesCount).IsRequired();
        builder.Property(cr => cr.FinalSalesTotalCount).IsRequired();
        builder.Property(cr => cr.DebitCount).IsRequired();
        builder.Property(cr => cr.MasterCardCount).IsRequired();
        builder.Property(cr => cr.VisaCount).IsRequired();
        builder.Property(cr => cr.AmexCount).IsRequired();
        builder.Property(cr => cr.DiscoverCount).IsRequired();
        builder.Property(cr => cr.OtherCreditCard1Count).IsRequired();
        builder.Property(cr => cr.TotalDebitCreditCount).IsRequired();
        builder.Property(cr => cr.TotalUsdollarsCount).IsRequired();
        builder.Property(cr => cr.TotalEuroDollarsCount).IsRequired();
        builder.Property(cr => cr.CashCount).IsRequired();
        builder.Property(cr => cr.Cash).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.FinalCashDepositCount).IsRequired();
        builder.Property(cr => cr.PaidInsCount).IsRequired();
        builder.Property(cr => cr.ReceiptsPaidOutsCount).IsRequired();
        builder.Property(cr => cr.IsReconciled).IsRequired();
        builder.Property(cr => cr.EosNickels).IsRequired();
        builder.Property(cr => cr.EosDimes).IsRequired();
        builder.Property(cr => cr.EosQuarters).IsRequired();
        builder.Property(cr => cr.EosLoonies).IsRequired();
        builder.Property(cr => cr.EosTwonies).IsRequired();
        builder.Property(cr => cr.EosFives).IsRequired();
        builder.Property(cr => cr.EosTens).IsRequired();
        builder.Property(cr => cr.EosTwentys).IsRequired();
        builder.Property(cr => cr.EosFiftys).IsRequired();
        builder.Property(cr => cr.EosOneHundreds).IsRequired();
        builder.Property(cr => cr.EosDepositTotal).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosDebit).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosVisa).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosMasterCard).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosAmex).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosDiscover).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosOtherCreditCard1).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosUsDollars).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosEuroDollars).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosCheques).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosFinalTotal).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosQuickCashDeposit).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosQuickDebitCredit).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosDebitCreditTotal).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.SalesStartTime).IsRequired();
        builder.Property(cr => cr.SalesEndTime).IsRequired(false);
        builder.Property(cr => cr.EosUsDollarsConverted).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.EosEuroDollarsConverted).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.CashDropCount).IsRequired();
        builder.Property(cr => cr.RefundsTotalPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.RefundsTotalCountPolled).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(cr => cr.RefundsTotal).HasColumnType("decimal(18, 2)").IsRequired();

        // Configure relationships
        builder.HasOne(cr => cr.SalesSession)
            .WithMany(ss => ss.CashierReconciliations)
            .HasForeignKey(cr => cr.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
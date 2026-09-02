using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        // Configure properties
        builder.Property(b => b.Id).HasColumnName("BookingId");
        builder.Property(b => b.TransactionId).IsRequired();
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(b => b.TimeCreated).IsRequired(false);
        builder.Property(b => b.CheckInTime).IsRequired();
        builder.Property(b => b.SubTotal).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.TotalDeposits).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Refunded).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Total).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.AmountPaid).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Tendered).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Change).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Cash).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Debit).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Amex).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.MasterCard).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Visa).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.DiscoveryCard).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.UnionPay).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Diners).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.TotalDiscount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.TaxRate).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Comment).IsRequired(false);
        builder.Property(b => b.RateDetails).IsRequired(false);
        builder.Property(b => b.OtherDiscountDollar).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.OtherDiscountReason).IsRequired(false).HasMaxLength(300);
        builder.Property(b => b.TotalTaxes).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.TimeCheckedOut).IsRequired(false);
        builder.Property(b => b.WakeUpCall).IsRequired(false);
        builder.Property(b => b.NonRefundedDeposits).HasColumnType("decimal(18,2)").IsRequired(false);
        builder.Property(b => b.RefundComment).IsRequired(false);
        builder.Property(b => b.RateTotal).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.RateDiscount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.MemberDiscount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Rate).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.CouponsDiscount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.PromoDiscount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.TimeCompleted).IsRequired(false);
        builder.Property(b => b.CheckedOutByEmployee).IsRequired(false);
        builder.Property(b => b.NewCreditCardNumber).IsRequired(false);
        builder.Property(b => b.NewCardExpiryDate).IsRequired(false);
        builder.Property(b => b.ShiftBookedIn).IsRequired();
        builder.Property(b => b.ShiftBookedOut).IsRequired();
        builder.Property(b => b.ReceiptMessage1).IsRequired(false);
        builder.Property(b => b.ReceiptMessage2).IsRequired(false);
        builder.Property(b => b.ReceiptCouponText).IsRequired(false);
        builder.Property(b => b.Disclaimer).IsRequired(false);
        builder.Property(b => b.USDollars).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.EuroDollars).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.USExchangeRate).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.EuroExchangeRate).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.MembershipSales).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.CheckInLoginId).IsRequired(false);
        builder.Property(b => b.CheckOutLoginId).IsRequired(false);
        builder.Property(b => b.NetAmount).HasColumnType("decimal(18,2)").IsRequired();

        // Configure relationships
        builder.HasOne(b => b.Transaction)
            .WithMany(t => t.Bookings)
            .HasForeignKey(b => b.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.BookingDetails)
            .WithOne(bd => bd.Booking)
            .HasForeignKey(bd => bd.BookingId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(b => b.Problems)
            .WithOne(p => p.Booking)
            .HasForeignKey(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        // Configure the properties
        builder.Property(a => a.Id).HasColumnName("TransactionId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(t => t.Sequence).IsRequired();
        builder.Property(t => t.UserId).HasMaxLength(500);
        builder.Property(t => t.NumOfGuests).IsRequired();
        builder.Property(t => t.TransactionStartTime).IsRequired();
        builder.Property(t => t.TotalDiscounts).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalDeposits).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalReturnedDeposits).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalNonReturnedDeposits).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalCoupons).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalRefunds).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.SubTotal).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.Taxes).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.GrandTotal).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.AmountTendered).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.Change).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalTransfer).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalVoids).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalCancelled).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.StoredOrder).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.IsVoid).IsRequired();
        builder.Property(t => t.Cash).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.Debit).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.Amex).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.MasterCard).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.Visa).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.DiscoveryCard).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.UnionPay).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.Diners).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalUSDollars).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.USExchangePaid).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.USExchangeReceived).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalMemberDiscounts).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalRateDiscounts).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalPromos).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalOtherDiscounts).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.RateTotal).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.TotalMembershipSales).HasColumnType("decimal(18,2)").IsRequired();

        // Configure the relationships
        builder.HasOne(t => t.Membership)
            .WithMany(m => m.Transactions)
            .HasForeignKey(t => t.MembershipId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.SalesType)
            .WithMany(st => st.Transactions)
            .HasForeignKey(t => t.SalesTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Bookings)
            .WithOne(b => b.Transaction)
            .HasForeignKey(b => b.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.MembershipSales)
            .WithOne(ms => ms.Transaction)
            .HasForeignKey(ms => ms.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.WaitingLists)
            .WithOne(wl => wl.Transaction)
            .HasForeignKey(wl => wl.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
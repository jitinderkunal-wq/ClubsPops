using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class MembershipSaleConfiguration : IEntityTypeConfiguration<MembershipSale>
{
    public void Configure(EntityTypeBuilder<MembershipSale> builder)
    {
        // Configure properties
        builder.HasKey(ms => ms.Id);
        builder.Property(ms => ms.Id).HasColumnName("MembershipSalesId");
        builder.Property(ms => ms.MembershipTypeId).IsRequired();
        builder.Property(ms => ms.SalesSessionId).IsRequired();
        builder.Property(ms => ms.MembershipPrice).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.MembershipSaleDate).IsRequired();
        builder.Property(ms => ms.Total).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.Tendered).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.Cash).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.Debit).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.Visa).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.MasterCard).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.Amex).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.Discover).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.OtherCreditCard).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.USExchangeRate).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.USDollars).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.EuroExchangeRate).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.EuroDollars).HasColumnType("decimal(18,2)");
        builder.Property(ms => ms.NewMemberNameReceipt).HasMaxLength(255);

        // Configure relationships
        builder.HasOne(ms => ms.MembershipType)
            .WithMany(mt => mt.MembershipSales)
            .HasForeignKey(ms => ms.MembershipTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ms => ms.SalesSession)
            .WithMany(ss => ss.MembershipSales)
            .HasForeignKey(ms => ms.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ms => ms.Transaction)
            .WithMany(t => t.MembershipSales)
            .HasForeignKey(ms => ms.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ms => ms.Problems)
            .WithOne(p => p.MembershipSale)
            .HasForeignKey(p => p.MembershipSaleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
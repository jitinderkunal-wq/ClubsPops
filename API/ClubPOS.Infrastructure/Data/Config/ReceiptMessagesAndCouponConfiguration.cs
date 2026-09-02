using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class ReceiptMessagesAndCouponConfiguration : IEntityTypeConfiguration<ReceiptMessagesAndCoupon>
{
    public void Configure(EntityTypeBuilder<ReceiptMessagesAndCoupon> builder)
    {
        // Configure properties
        builder.HasKey(rmc => rmc.Id);
        builder.Property(rmc => rmc.Id).HasColumnName("ReceiptMessagesAndCouponId");
        builder.Property(rmc => rmc.Disclaimer).HasMaxLength(500).IsRequired(true);
        builder.Property(rmc => rmc.Message1).HasMaxLength(200).IsRequired(true);
        builder.Property(rmc => rmc.Message2).HasMaxLength(200);
        builder.Property(rmc => rmc.CouponText).HasMaxLength(100);
        builder.Property(rmc => rmc.Address).HasMaxLength(300);
        builder.Property(rmc => rmc.GSTNumber).HasMaxLength(20);
    }
}
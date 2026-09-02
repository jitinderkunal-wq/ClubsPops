using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        // Configure properties
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("CouponId");
        builder.Property(c => c.AccommodationTypeId).IsRequired(false);
        builder.Property(c => c.DateCreated).IsRequired();
        builder.Property(c => c.CouponName).HasMaxLength(250).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(1000);
        builder.Property(c => c.CouponNumber).IsRequired();
        builder.Property(c => c.FlatRate).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(c => c.DiscountDollars).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(c => c.DiscountPercent).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(c => c.StartDate).IsRequired(false);
        builder.Property(c => c.ExpiryDate).IsRequired(false);
        builder.Property(c => c.IsExclusive);
        builder.Property(c => c.IsActive);
        builder.Property(c => c.IsCoupon);
        builder.Property(c => c.ReturnOnCheckOut);

        // Configure relationships
        builder.HasOne(c => c.AccommodationType)
            .WithMany(at => at.Coupons)
            .HasForeignKey(c => c.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.BookingDetailCoupons)
            .WithOne(bd => bd.Coupon)
            .HasForeignKey(bd => bd.CouponId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.BookingDetailPromopasses)
            .WithOne(bd => bd.Promopass)
            .HasForeignKey(bd => bd.PromopassId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.CouponDetails)
            .WithOne(cd => cd.Coupon)
            .HasForeignKey(cd => cd.CouponId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
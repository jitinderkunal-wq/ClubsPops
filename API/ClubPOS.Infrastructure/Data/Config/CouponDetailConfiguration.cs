using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class CouponDetailConfiguration : IEntityTypeConfiguration<CouponDetail>
{
    public void Configure(EntityTypeBuilder<CouponDetail> builder)
    {
        // Configure properties
        builder.HasKey(cd => cd.Id);
        builder.Property(cd => cd.Id).HasColumnName("CouponDetailId");
        builder.Property(cd => cd.CouponId).IsRequired();
        builder.Property(cd => cd.AccommodationRateId).IsRequired();

        // Configure relationships
        builder.HasOne(cd => cd.Coupon)
            .WithMany(c => c.CouponDetails)
            .HasForeignKey(cd => cd.CouponId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cd => cd.AccommodationRate)
            .WithMany(ar => ar.CouponDetails)
            .HasForeignKey(cd => cd.AccommodationRateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
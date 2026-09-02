using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class BookingDetailConfiguration : IEntityTypeConfiguration<BookingDetail>
{
    public void Configure(EntityTypeBuilder<BookingDetail> builder)
    {
        builder.HasKey(b => b.Id);

        // Configure properties
        builder.Property(b => b.Id).HasColumnName("BookingDetailId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(b => b.AccommodationId).IsRequired();
        builder.Property(b => b.BookingId).IsRequired();
        builder.Property(b => b.HoursOfStay).IsRequired();
        builder.Property(b => b.Qty).IsRequired();
        builder.Property(b => b.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.HasDeposit).IsRequired();
        builder.Property(b => b.DiscountFactor).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.NonMemberDiscount).HasColumnType("decimal(18,2)");
        builder.Property(b => b.PriceDiscount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.Returned).IsRequired();
        builder.Property(b => b.Transfered).IsRequired();
        builder.Property(b => b.MemberDiscountDetail).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.AccommodationCouponDiscount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.AccommodationPromopassDiscount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.TransferDepositDetail).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.TransferOut).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.TransferDepositsReturned).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.AccommodationOtherDiscount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.AccommodationOtherDisPercent).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.ChargableDeposit).IsRequired();
        builder.Property(b => b.DepositTransRefund).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.IsKeyRefundedOnly).IsRequired();

        // Configure relationships
        builder.HasOne(bd => bd.Accommodation)
            .WithMany(a => a.BookingDetails)
            .HasForeignKey(bd => bd.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bd => bd.AccommodationItem)
            .WithMany(ai => ai.BookingDetails)
            .HasForeignKey(bd => bd.AccommodationItemId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(bd => bd.AccommodationRate)
            .WithMany(ar => ar.BookingDetails)
            .HasForeignKey(bd => bd.AccommodationRateId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(bd => bd.Booking)
            .WithMany(b => b.BookingDetails)
            .HasForeignKey(bd => bd.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bd => bd.BookingDetailStatus)
            .WithMany(bds => bds.BookingDetails)
            .HasForeignKey(bd => bd.BookingDetailStatusId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(bd => bd.Coupon)
            .WithMany(c => c.BookingDetailCoupons)
            .HasForeignKey(bd => bd.CouponId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(bd => bd.Promopass)
            .WithMany(p => p.BookingDetailPromopasses)
            .HasForeignKey(bd => bd.PromopassId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(bd => bd.DiscountRate)
            .WithMany(dr => dr.BookingDetails)
            .HasForeignKey(bd => bd.DiscountRateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(bd => bd.Membership)
            .WithMany(m => m.BookingDetails)
            .HasForeignKey(bd => bd.MembershipId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
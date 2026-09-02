using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class DiscountRateConfiguration : IEntityTypeConfiguration<DiscountRate>
{
    public void Configure(EntityTypeBuilder<DiscountRate> builder)
    {
        // Configure properties
        builder.HasKey(dr => dr.Id);
        builder.Property(dr => dr.Id).HasColumnName("DiscountRateId");

        builder.Property(dr => dr.AccommodationTypeId).IsRequired();
        builder.Property(dr => dr.RateName).IsRequired(false);
        builder.Property(dr => dr.DiscountAmount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(dr => dr.DiscountPercent).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(dr => dr.DiscountFlatRate).HasColumnType("decimal(18,2)").IsRequired();

        // Configure relationships
        builder.HasOne(dr => dr.AccommodationType)
            .WithMany(at => at.DiscountRates)
            .HasForeignKey(dr => dr.AccommodationTypeId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(dr => dr.BookingDetails)
            .WithOne(bd => bd.DiscountRate)
            .HasForeignKey(bd => bd.DiscountRateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
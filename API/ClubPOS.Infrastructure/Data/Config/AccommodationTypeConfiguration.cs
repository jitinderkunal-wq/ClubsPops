using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class AccommodationTypeConfiguration : IEntityTypeConfiguration<AccommodationType>
{
    public void Configure(EntityTypeBuilder<AccommodationType> builder)
    {
        builder.HasKey(at => at.Id);
        builder.Property(at => at.Id).HasColumnName("AccommodationTypeId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(at => at.TypeName).HasMaxLength(50).IsRequired();
        builder.Property(at => at.MaxOccupants).IsRequired();
        builder.Property(at => at.CompatibilityValue).IsRequired();

        // Configure the relationships with other entities

        builder.HasMany(at => at.Accommodations)
            .WithOne(a => a.AccommodationType)
            .HasForeignKey(a => a.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(at => at.AccommodationRates)
            .WithOne(ar => ar.AccommodationType)
            .HasForeignKey(ar => ar.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(at => at.AccommodationSubItems)
            .WithOne(asi => asi.AccommodationType)
            .HasForeignKey(asi => asi.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(at => at.Coupons)
            .WithOne(c => c.AccommodationType)
            .HasForeignKey(c => c.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(at => at.DiscountRates)
            .WithOne(dr => dr.AccommodationType)
            .HasForeignKey(dr => dr.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(at => at.WaitingLists)
            .WithOne(wl => wl.AccommodationType)
            .HasForeignKey(c => c.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
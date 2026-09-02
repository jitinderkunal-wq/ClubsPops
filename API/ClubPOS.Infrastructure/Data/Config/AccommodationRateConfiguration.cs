using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class AccommodationRateConfiguration : IEntityTypeConfiguration<AccommodationRate>
{
      public void Configure(EntityTypeBuilder<AccommodationRate> builder)
      {
            builder.HasKey(ar => ar.Id);
            builder.Property(ar => ar.Id).HasColumnName("AccommodationRateId");
            builder.Property(a => a.OrganizationId).IsRequired();
            builder.Property(ar => ar.RateName).HasMaxLength(50).IsRequired();
            builder.Property(ar => ar.Price).HasColumnType("decimal(18,2)");
            builder.Property(ar => ar.StartDayOfTheWeek).HasMaxLength(100).IsRequired(false);
            builder.Property(ar => ar.RateStartTime).IsRequired(false);
            builder.Property(ar => ar.EndDayOfWeek).HasMaxLength(100).IsRequired(false);
            builder.Property(ar => ar.RateEndTime).IsRequired(false);
            builder.Property(ar => ar.IsStandardRate).IsRequired();
            builder.Property(ar => ar.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(ar => ar.IsActive).IsRequired();

            builder.HasOne(ar => ar.AccommodationType)
                  .WithMany(at => at.AccommodationRates)
                  .HasForeignKey(ar => ar.AccommodationTypeId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ar => ar.Tax1Navigation)
                  .WithMany(t => t.AccommodationRateTax1Navigations)
                  .HasForeignKey(ar => ar.Tax1)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ar => ar.Tax2Navigation)
                  .WithMany(t => t.AccommodationRateTax2Navigations)
                  .HasForeignKey(ar => ar.Tax2)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ar => ar.Tax3Navigation)
                  .WithMany(t => t.AccommodationRateTax3Navigations)
                  .HasForeignKey(ar => ar.Tax3)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ar => ar.Tax4Navigation)
                  .WithMany(t => t.AccommodationRateTax4Navigations)
                  .HasForeignKey(ar => ar.Tax4)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ar => ar.Tax5Navigation)
                  .WithMany(t => t.AccommodationRateTax5Navigations)
                  .HasForeignKey(ar => ar.Tax5)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ar => ar.BookingDetails)
                  .WithOne(bd => bd.AccommodationRate)
                  .HasForeignKey(bd => bd.AccommodationRateId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ar => ar.CouponDetails)
                  .WithOne(cd => cd.AccommodationRate)
                  .HasForeignKey(cd => cd.AccommodationRateId)
                  .OnDelete(DeleteBehavior.Cascade);
      }
}
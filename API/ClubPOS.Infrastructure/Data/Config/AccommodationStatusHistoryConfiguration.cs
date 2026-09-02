using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class AccommodationStatusHistoryConfiguration : IEntityTypeConfiguration<AccommodationStatusHistory>
{
    public void Configure(EntityTypeBuilder<AccommodationStatusHistory> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(a => a.Id).HasColumnName("AccommodationStatusHistoryId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(h => h.StatusChangeDate).IsRequired();

        builder.HasOne(h => h.Accommodation)
            .WithMany(a => a.AccommodationStatusHistories)
            .HasForeignKey(h => h.AccommodationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.AccommodationStatus)
            .WithMany(s => s.AccommodationStatusHistories)
            .HasForeignKey(h => h.AccommodationStatusId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
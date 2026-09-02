using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class AccommodationStatusConfiguration : IEntityTypeConfiguration<AccommodationStatus>
{
    public void Configure(EntityTypeBuilder<AccommodationStatus> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(a => a.Id).HasColumnName("AccommodationStatusId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(s => s.StatusName).IsRequired();

        builder.HasMany(s => s.Accommodations)
            .WithOne(a => a.AccommodationStatus)
            .HasForeignKey(a => a.AccommodationStatusId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.AccommodationStatusHistories)
            .WithOne(h => h.AccommodationStatus)
            .HasForeignKey(h => h.AccommodationStatusId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
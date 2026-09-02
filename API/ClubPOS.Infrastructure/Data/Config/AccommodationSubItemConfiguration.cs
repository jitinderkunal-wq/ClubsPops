using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class AccommodationSubItemConfiguration : IEntityTypeConfiguration<AccommodationSubItem>
{
    public void Configure(EntityTypeBuilder<AccommodationSubItem> builder)
    {
        builder.HasKey(s => new { s.AccommodationTypeId, s.AccommodationItemId });
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(s => s.IsRequired).IsRequired();
        builder.Property(s => s.MaxQty).IsRequired();
        builder.Property(s => s.IsActive).IsRequired();

        builder.HasOne(s => s.AccommodationItem)
            .WithMany(a => a.AccommodationSubItems)
            .HasForeignKey(s => s.AccommodationItemId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.AccommodationType)
            .WithMany(t => t.AccommodationSubItems)
            .HasForeignKey(s => s.AccommodationTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
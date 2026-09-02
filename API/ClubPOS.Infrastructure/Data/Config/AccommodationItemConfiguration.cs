using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class AccommodationItemConfiguration : IEntityTypeConfiguration<AccommodationItem>
{
    public void Configure(EntityTypeBuilder<AccommodationItem> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("AccommodationItemId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(a => a.ItemName).IsRequired();
        builder.Property(a => a.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(a => a.DepositAmount).HasColumnType("decimal(18,2)");
        builder.Property(a => a.IsActive).IsRequired();

        builder.HasMany(a => a.AccommodationSubItems)
            .WithOne(s => s.AccommodationItem)
            .HasForeignKey(s => s.AccommodationItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.BookingDetails)
            .WithOne(bd => bd.AccommodationItem)
            .HasForeignKey(bd => bd.AccommodationItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
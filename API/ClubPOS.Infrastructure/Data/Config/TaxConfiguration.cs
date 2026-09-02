using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class TaxConfiguration : IEntityTypeConfiguration<Tax>
{
    public void Configure(EntityTypeBuilder<Tax> builder)
    {
        builder.HasKey(t => t.Id);

        // Configure properties
        builder.Property(t => t.Id).HasColumnName("TaxId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(t => t.TaxName).HasMaxLength(50).IsRequired();
        builder.Property(t => t.TaxRate).HasColumnType("decimal(18,2)").IsRequired();

        // Configure relationships
        builder.HasMany(t => t.AccommodationRateTax1Navigations)
               .WithOne(ar => ar.Tax1Navigation)
               .HasForeignKey(ar => ar.Tax1)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.AccommodationRateTax2Navigations)
               .WithOne(ar => ar.Tax2Navigation)
               .HasForeignKey(ar => ar.Tax2)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.AccommodationRateTax3Navigations)
               .WithOne(ar => ar.Tax3Navigation)
               .HasForeignKey(ar => ar.Tax3)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.AccommodationRateTax4Navigations)
               .WithOne(ar => ar.Tax4Navigation)
               .HasForeignKey(ar => ar.Tax4)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.AccommodationRateTax5Navigations)
               .WithOne(ar => ar.Tax5Navigation)
               .HasForeignKey(ar => ar.Tax5)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
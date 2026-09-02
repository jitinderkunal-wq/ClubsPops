using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class ShiftSetUpConfiguration : IEntityTypeConfiguration<ShiftSetUp>
{
    public void Configure(EntityTypeBuilder<ShiftSetUp> builder)
    {
        // Configure properties
        builder.HasKey(ss => ss.Id);
        builder.Property(ss => ss.Id).HasColumnName("ShiftSetupId");
        builder.Property(ss => ss.ShiftNumber).IsRequired();
        builder.Property(ss => ss.ShiftStart).IsRequired();
        builder.Property(ss => ss.ShiftEnd).IsRequired();
        builder.Property(ss => ss.IsActive);

        // Configure relationships
    }
}
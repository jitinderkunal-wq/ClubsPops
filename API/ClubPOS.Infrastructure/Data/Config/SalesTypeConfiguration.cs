using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class SalesTypeConfiguration : IEntityTypeConfiguration<SalesType>
{
    public void Configure(EntityTypeBuilder<SalesType> builder)
    {
        builder.HasKey(st => st.Id);

        // Configure properties
        builder.Property(b => b.Id).HasColumnName("SalesTypeId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(st => st.SalesTypeName).IsRequired();

        // Configure relationships
        builder.HasMany(st => st.Transactions)
            .WithOne(t => t.SalesType)
            .HasForeignKey(t => t.SalesTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(o => o.OrganizationId);
        builder.Property(o => o.OrganizationName).HasMaxLength(255).IsRequired();
        builder.Property(o => o.Street).HasMaxLength(255);
        builder.Property(o => o.City).HasMaxLength(100);
        builder.Property(o => o.Province).HasMaxLength(100);
        builder.Property(o => o.PostalCode).HasMaxLength(20);
        builder.Property(o => o.Country).HasMaxLength(100);
        builder.Property(o => o.RegistrationNumber).HasMaxLength(50).IsRequired();
        builder.Property(o => o.Logo).HasMaxLength(255).IsRequired();
    }
}
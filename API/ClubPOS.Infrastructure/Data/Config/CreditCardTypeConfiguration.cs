using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class CreditCardTypeConfiguration : IEntityTypeConfiguration<CreditCardType>
{
    public void Configure(EntityTypeBuilder<CreditCardType> builder)
    {
        // Configure properties
        builder.HasKey(cct => cct.Id);
        builder.Property(cct => cct.Id).HasColumnName("CreditCardTypeId");
        builder.Property(cct => cct.CreditCardTypeName).HasMaxLength(250).IsRequired();

        // Configure relationships
        builder.HasMany(cct => cct.Memberships)
            .WithOne(m => m.CreditCardType)
            .HasForeignKey(m => m.CreditCardTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
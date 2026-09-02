using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        // Configure properties
        builder.HasKey(er => er.Id);
        builder.Property(er => er.Id).HasColumnName("ExchangeRateId");
        builder.Property(er => er.RateName).HasMaxLength(250).IsRequired();
        builder.Property(er => er.Rate).HasColumnType("decimal(18,2)");

        // Configure relationships
    }
}
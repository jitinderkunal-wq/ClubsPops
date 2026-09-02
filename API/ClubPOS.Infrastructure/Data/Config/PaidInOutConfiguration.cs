using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class PaidInOutConfiguration : IEntityTypeConfiguration<PaidInOut>
{
    public void Configure(EntityTypeBuilder<PaidInOut> builder)
    {
        builder.HasKey(pio => pio.Id);

        // Configure properties
        builder.Property(pio => pio.Id).HasColumnName("PaidInOutId");
        builder.Property(pio => pio.SalesSessionId).IsRequired();
        builder.Property(pio => pio.DateCreated).IsRequired();
        builder.Property(pio => pio.PaidIn).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(pio => pio.PaidInComment).HasMaxLength(255).IsRequired(false);
        builder.Property(pio => pio.PaidOut).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(pio => pio.PaidOutComment).HasMaxLength(255).IsRequired(false);

        // Configure relationships
        builder.HasOne(pio => pio.SalesSession)
            .WithMany(ss => ss.PaidInOuts)
            .HasForeignKey(pio => pio.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
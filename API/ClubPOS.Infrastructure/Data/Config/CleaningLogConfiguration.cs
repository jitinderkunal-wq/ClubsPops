using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class CleaningLogConfiguration : IEntityTypeConfiguration<CleaningLog>
{
    public void Configure(EntityTypeBuilder<CleaningLog> builder)
    {
        // Configure properties
        builder.HasKey(cl => cl.Id);
        builder.Property(cl => cl.Id).HasColumnName("CleaningLogId");
        builder.Property(cl => cl.AccommodationId).IsRequired();
        builder.Property(cl => cl.UserId).HasMaxLength(500);
        builder.Property(cl => cl.LogDate).IsRequired();
        builder.Property(cl => cl.Comment).HasMaxLength(1000);

        // Configure relationships
        builder.HasOne(cl => cl.Accommodation)
            .WithMany(a => a.CleaningLogs)
            .HasForeignKey(cl => cl.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(cl => cl.CleaningLogDetails)
            .WithOne(cld => cld.CleaningLog)
            .HasForeignKey(cld => cld.CleaningLogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config
{
    public class MaintenanceLogConfiguration : IEntityTypeConfiguration<MaintenanceLog>
    {
        public void Configure(EntityTypeBuilder<MaintenanceLog> builder)
        {
            // Configure properties
            builder.HasKey(ml => ml.Id);
            builder.Property(ml => ml.Id).HasColumnName("MaintenanceLogId");
            builder.Property(ml => ml.AccommodationId).IsRequired();
            builder.Property(ml => ml.UserId).HasMaxLength(500).IsRequired();
            builder.Property(ml => ml.LogDate).IsRequired();
            builder.Property(ml => ml.BackInServiceDate).IsRequired(false);
            builder.Property(ml => ml.Comment).HasMaxLength(300).IsRequired(false);
            builder.Property(ml => ml.ResolvedComment).HasMaxLength(300).IsRequired(false);
            builder.Property(ml => ml.NeedsCleaning).IsRequired();
            builder.Property(ml => ml.ResolvedUserId).HasMaxLength(500);

            // Configure relationships
            builder.HasOne(ml => ml.Accommodation)
                .WithMany(a => a.MaintenanceLogs)
                .HasForeignKey(ml => ml.AccommodationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ml => ml.MaintenanceLogDetails)
                .WithOne(mld => mld.MaintenanceLog)
                .HasForeignKey(mld => mld.MaintenanceLogId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
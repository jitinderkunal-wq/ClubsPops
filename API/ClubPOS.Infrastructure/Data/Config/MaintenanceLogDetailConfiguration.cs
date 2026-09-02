using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class MaintenanceLogDetailConfiguration : IEntityTypeConfiguration<MaintenanceLogDetail>
{
    public void Configure(EntityTypeBuilder<MaintenanceLogDetail> builder)
    {
        // Configure properties
        builder.HasKey(mld => mld.Id);
        builder.Property(mld => mld.Id).HasColumnName("MaintenanceLogDetailId");

        // Configure relationships
        builder.HasOne(mld => mld.MaintenanceLog)
            .WithMany(ml => ml.MaintenanceLogDetails)
            .HasForeignKey(mld => mld.MaintenanceLogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mld => mld.MaintenanceTask)
            .WithMany(mt => mt.MaintenanceLogDetails)
            .HasForeignKey(mld => mld.MaintenanceTaskId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
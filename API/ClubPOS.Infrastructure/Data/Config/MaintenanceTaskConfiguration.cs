using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class MaintenanceTaskConfiguration : IEntityTypeConfiguration<MaintenanceTask>
{
    public void Configure(EntityTypeBuilder<MaintenanceTask> builder)
    {
        // Configure properties
        builder.HasKey(mt => mt.Id);
        builder.Property(mt => mt.Id).HasColumnName("MaintenanceTaskId");
        builder.Property(mt => mt.TaskName).IsRequired();

        // Configure relationships
        builder.HasMany(mt => mt.MaintenanceLogDetails)
            .WithOne(mld => mld.MaintenanceTask)
            .HasForeignKey(mld => mld.MaintenanceTaskId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
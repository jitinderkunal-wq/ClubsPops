using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class CleaningTaskConfiguration : IEntityTypeConfiguration<CleaningTask>
{
    public void Configure(EntityTypeBuilder<CleaningTask> builder)
    {
        // Configure properties
        builder.HasKey(ct => ct.Id);
        builder.Property(ct => ct.Id).HasColumnName("CleaningTaskId");
        builder.Property(ct => ct.TaskName).HasMaxLength(100).IsRequired();
        builder.Property(ct => ct.IsActive).IsRequired();

        // Configure relationships
        builder.HasMany(ct => ct.CleaningLogDetails)
            .WithOne(cld => cld.CleaningTask)
            .HasForeignKey(cld => cld.CleaningTaskId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
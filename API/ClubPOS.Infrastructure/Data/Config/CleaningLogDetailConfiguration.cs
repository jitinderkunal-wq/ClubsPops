using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class CleaningLogDetailConfiguration : IEntityTypeConfiguration<CleaningLogDetail>
{
    public void Configure(EntityTypeBuilder<CleaningLogDetail> builder)
    {
        // Configure properties
        builder.HasKey(cld => cld.Id);
        builder.Property(cld => cld.Id).HasColumnName("CleaningLogDetailId");
        builder.Property(cld => cld.CleaningLogId).IsRequired();
        builder.Property(cld => cld.CleaningTaskId).IsRequired();

        // Configure relationships
        builder.HasOne(cld => cld.CleaningLog)
            .WithMany(cl => cl.CleaningLogDetails)
            .HasForeignKey(cld => cld.CleaningLogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cld => cld.CleaningTask)
            .WithMany(ct => ct.CleaningLogDetails)
            .HasForeignKey(cld => cld.CleaningTaskId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class ProblemCategoryConfiguration : IEntityTypeConfiguration<ProblemCategory>
{
    public void Configure(EntityTypeBuilder<ProblemCategory> builder)
    {
        builder.ToTable("ProblemCategories");
        // Configure properties
        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.Id).HasColumnName("ProblemCategoryId");
        builder.Property(pc => pc.ProblemCategoryName).HasMaxLength(50).IsRequired();

        // Configure relationships
        builder.HasMany(pc => pc.Problems)
            .WithOne(p => p.ProblemCategory)
            .HasForeignKey(p => p.ProblemCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
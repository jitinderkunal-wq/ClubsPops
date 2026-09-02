using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.ToTable("Problems");

        builder.HasKey(p => p.Id);

        // Configure properties
        builder.Property(p => p.Id).HasColumnName("ProblemId");
        builder.Property(p => p.BookingId).IsRequired(false);
        builder.Property(p => p.MembershipId).IsRequired(false);
        builder.Property(p => p.MembershipSaleId).IsRequired(false);
        builder.Property(p => p.ProblemCategoryId).IsRequired();
        builder.Property(p => p.SalesSessionId).IsRequired(false);
        builder.Property(p => p.ProblemExplaination).HasMaxLength(200).IsRequired();
        builder.Property(p => p.UserId).HasMaxLength(500).IsRequired(false);
        builder.Property(p => p.DateCreated).IsRequired();
        builder.Property(p => p.NewMemberName).HasMaxLength(100).IsRequired(false);

        // Configure relationships
        builder.HasOne(p => p.Booking)
            .WithMany(b => b.Problems)
            .HasForeignKey(p => p.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Membership)
            .WithMany(m => m.Problems)
            .HasForeignKey(p => p.MembershipId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.MembershipSale)
            .WithMany(ms => ms.Problems)
            .HasForeignKey(p => p.MembershipSaleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.ProblemCategory)
            .WithMany(pc => pc.Problems)
            .HasForeignKey(p => p.ProblemCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.SalesSession)
            .WithMany(ss => ss.Problems)
            .HasForeignKey(p => p.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
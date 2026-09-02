using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class SalesSessionConfiguration : IEntityTypeConfiguration<SalesSession>
{
    public void Configure(EntityTypeBuilder<SalesSession> builder)
    {
        builder.HasKey(ss => ss.Id);

        // Configure properties
        builder.Property(ss => ss.Id).HasColumnName("SalesSessionId");
        builder.Property(ss => ss.AppUserId).HasMaxLength(500).IsRequired();
        builder.Property(ss => ss.SessionStartDate).IsRequired();
        builder.Property(ss => ss.SessionEndDate).IsRequired(false);
        builder.Property(ss => ss.ShiftNumber).IsRequired();
        builder.Property(ss => ss.BeginFloat).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(ss => ss.DrawerNumber).IsRequired();
        builder.Property(ss => ss.BusinessDay).IsRequired();
        builder.Property(ss => ss.IsCurrentlyInUse).IsRequired();

        // Configure relationships
        builder.HasMany(ss => ss.CashierReconciliations)
            .WithOne(cr => cr.SalesSession)
            .HasForeignKey(cr => cr.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(ss => ss.DrawerSkims)
            .WithOne(ds => ds.SalesSession)
            .HasForeignKey(ds => ds.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(ss => ss.MembershipSales)
            .WithOne(ms => ms.SalesSession)
            .HasForeignKey(ms => ms.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(ss => ss.PaidInOuts)
            .WithOne(pio => pio.SalesSession)
            .HasForeignKey(pio => pio.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(ss => ss.Problems)
            .WithOne(p => p.SalesSession)
            .HasForeignKey(p => p.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
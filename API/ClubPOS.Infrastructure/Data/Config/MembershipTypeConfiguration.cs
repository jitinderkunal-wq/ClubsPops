using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class MembershipTypeConfiguration : IEntityTypeConfiguration<MembershipType>
{
    public void Configure(EntityTypeBuilder<MembershipType> builder)
    {
        // Configure properties
        builder.HasKey(mt => mt.Id);
        builder.Property(mt => mt.Id).HasColumnName("MembershipTypeId");
        builder.Property(mt => mt.MembershipTypeName).HasMaxLength(250).IsRequired();
        builder.Property(mt => mt.DiscountFactor).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(mt => mt.DiscountAmount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(mt => mt.MemberShipPrice).HasColumnType("decimal(18,2)").IsRequired();

        // Configure relationships
        builder.HasMany(mt => mt.Memberships)
            .WithOne(m => m.MembershipType)
            .HasForeignKey(m => m.MembershipTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(mt => mt.MembershipSales)
            .WithOne(ms => ms.MembershipType)
            .HasForeignKey(ms => ms.MembershipTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(mt => mt.WaitingLists)
            .WithOne(wl => wl.MembershipType)
            .HasForeignKey(wl => wl.MembershipTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
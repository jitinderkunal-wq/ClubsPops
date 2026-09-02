using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        // Configure properties
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("MembershipId");
        builder.Property(m => m.MembershipTypeId).IsRequired();
        builder.Property(m => m.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(m => m.LastName).HasMaxLength(100);
        builder.Property(m => m.Address).HasMaxLength(500);
        builder.Property(m => m.Phone).HasMaxLength(20);
        builder.Property(m => m.City).HasMaxLength(200);
        builder.Property(m => m.Province).HasMaxLength(200);
        builder.Property(m => m.Country).HasMaxLength(200);
        builder.Property(m => m.PostalZipCode).HasMaxLength(20);
        builder.Property(m => m.MembershipStartDate).IsRequired();
        builder.Property(m => m.CreditCardNumber).HasMaxLength(50);
        builder.Property(m => m.IsActive).IsRequired();

        // Configure relationships
        builder.HasOne(m => m.CreditCardType)
            .WithMany(cct => cct.Memberships)
            .HasForeignKey(m => m.CreditCardTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.MembershipType)
            .WithMany(mt => mt.Memberships)
            .HasForeignKey(m => m.MembershipTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.BookingDetails)
            .WithOne(bd => bd.Membership)
            .HasForeignKey(bd => bd.MembershipId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Problems)
            .WithOne(p => p.Membership)
            .HasForeignKey(p => p.MembershipId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Transactions)
            .WithOne(t => t.Membership)
            .HasForeignKey(t => t.MembershipId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.WaitingLists)
            .WithOne(wl => wl.Membership)
            .HasForeignKey(wl => wl.MembershipId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
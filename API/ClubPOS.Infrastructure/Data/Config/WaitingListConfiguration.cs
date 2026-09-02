using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class WaitingListConfiguration : IEntityTypeConfiguration<WaitingList>
{
    public void Configure(EntityTypeBuilder<WaitingList> builder)
    {
        // Configure properties
        builder.HasKey(wl => wl.Id);
        builder.Property(wl => wl.Id).HasColumnName("WaitingListId");
        builder.Property(wl => wl.AccommodationId).IsRequired();
        builder.Property(wl => wl.MembershipTypeId).IsRequired();
        builder.Property(wl => wl.TransactionId).IsRequired();
        builder.Property(wl => wl.UserId).HasMaxLength(500);
        builder.Property(wl => wl.Phone).HasMaxLength(20);
        builder.Property(wl => wl.Comment).HasMaxLength(500);
        builder.Property(wl => wl.IsActive).IsRequired();

        // Configure relationships
        builder.HasOne(wl => wl.Accommodation)
            .WithMany(a => a.WaitingListAccommodations)
            .HasForeignKey(wl => wl.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wl => wl.AccommodationType)
            .WithMany(at => at.WaitingLists)
            .HasForeignKey(wl => wl.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(wl => wl.Membership)
            .WithMany(m => m.WaitingLists)
            .HasForeignKey(wl => wl.MembershipId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(wl => wl.MembershipType)
            .WithMany(mt => mt.WaitingLists)
            .HasForeignKey(wl => wl.MembershipTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wl => wl.RoomAssigned)
            .WithMany(a => a.WaitingListRoomAssigneds)
            .HasForeignKey(wl => wl.RoomAssignedId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(wl => wl.Transaction)
            .WithMany(t => t.WaitingLists)
            .HasForeignKey(wl => wl.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class AccommodationConfiguration : IEntityTypeConfiguration<Accommodation>
{
    public void Configure(EntityTypeBuilder<Accommodation> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("AccommodationId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(a => a.AccommodationTypeId).IsRequired();
        builder.Property(a => a.Number).IsRequired().HasMaxLength(50);
        builder.Property(a => a.Floor).IsRequired();
        builder.Property(a => a.DeActivate);
        builder.Property(a => a.RequiresCreditCard);
        builder.Property(a => a.HasMaintenanceReservation);
        builder.Property(a => a.IsGhost);
        builder.Property(a => a.HasGloryHole);

        builder.HasOne(a => a.AccommodationType)
            .WithMany(t => t.Accommodations)
            .HasForeignKey(a => a.AccommodationTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.AccommodationStatus)
            .WithMany(s => s.Accommodations)
            .HasForeignKey(a => a.AccommodationStatusId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(a => a.AccommodationStatusHistories)
            .WithOne(h => h.Accommodation)
            .HasForeignKey(h => h.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.BookingDetails)
            .WithOne(bd => bd.Accommodation)
            .HasForeignKey(bd => bd.AccommodationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.MaintenanceLogs)
            .WithOne(ml => ml.Accommodation)
            .HasForeignKey(ml => ml.AccommodationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.CleaningLogs)
            .WithOne(cl => cl.Accommodation)
            .HasForeignKey(cl => cl.AccommodationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.WaitingListAccommodations)
            .WithOne(wl => wl.Accommodation)
            .HasForeignKey(wl => wl.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.WaitingListRoomAssigneds)
            .WithOne(wl => wl.RoomAssigned)
            .HasForeignKey(wl => wl.RoomAssignedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
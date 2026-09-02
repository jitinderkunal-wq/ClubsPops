using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class BookingDetailStatusConfiguration : IEntityTypeConfiguration<BookingDetailStatus>
{
    public void Configure(EntityTypeBuilder<BookingDetailStatus> builder)
    {
        builder.HasKey(bds => bds.Id);

        // Configure properties
        builder.Property(b => b.Id).HasColumnName("BookingDetailStatusId");
        builder.Property(a => a.OrganizationId).IsRequired();
        builder.Property(bds => bds.BookingDetailStatusName).IsRequired();

        // Configure the relationships
        builder.HasMany(bds => bds.BookingDetails)
            .WithOne(bd => bd.BookingDetailStatus)
            .HasForeignKey(bd => bd.BookingDetailStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
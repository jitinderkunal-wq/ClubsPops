using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubPOS.Infrastructure.Data.Config;

public class DrawerSkimConfiguration : IEntityTypeConfiguration<DrawerSkim>
{
    public void Configure(EntityTypeBuilder<DrawerSkim> builder)
    {
        builder.HasKey(ds => ds.Id);

        // Configure properties
        builder.Property(ds => ds.Id).HasColumnName("DrawerSkimId");
        builder.Property(ds => ds.SalesSessionId).IsRequired();
        builder.Property(ds => ds.SkimDate).IsRequired();
        builder.Property(ss => ss.TotalCoin).HasColumnType("decimal(18,2)");
        builder.Property(ss => ss.TotalCheques).HasColumnType("decimal(18,2)");
        builder.Property(ss => ss.TotalOther).HasColumnType("decimal(18,2)");
        builder.Property(ss => ss.TotalEuros).HasColumnType("decimal(18,2)");
        builder.Property(ss => ss.CoinTotal).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total1Dollar).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total2Dollar).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total5Dollar).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total10Dollar).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total20Dollar).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total50Dollar).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total100Dollar).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total1DollarUs).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total2DollarUs).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total5DollarUs).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total10DollarUs).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total20DollarUs).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total50DollarUs).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.Total100DollarUs).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.ChequeTotal).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.OtherTotal).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.EuroTotal).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.TotalUsdollars).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.TotalCaddollar).HasColumnType("decimal(18,2)").IsRequired(true);
        builder.Property(ss => ss.GrandTotal).HasColumnType("decimal(18,2)").IsRequired(true);

        // Configure relationships
        builder.HasOne(ds => ds.SalesSession)
            .WithMany(ss => ss.DrawerSkims)
            .HasForeignKey(ds => ds.SalesSessionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
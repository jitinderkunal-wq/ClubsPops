using ClubPOS.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClubPOS.Infrastructure.Identity;

public class IdentityDbContext : IdentityDbContext<AppUser>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>(appUser =>
        {
            appUser.HasKey(au => au.Id);
            appUser.Property(au => au.Id).HasColumnName("AppUserId");
            appUser.Property(au => au.FirstName).HasMaxLength(150).IsRequired(true);
            appUser.Property(au => au.LastName).HasMaxLength(150);
            appUser.Property(au => au.PayRate).HasColumnType("decimal(18, 2)");
            appUser.Property(au => au.HireDate).IsRequired(true);
        });
    }
}
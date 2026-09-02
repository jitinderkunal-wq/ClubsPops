using System.Reflection;
using ClubPOS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubPOS.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<AccommodationType>? AccommodationTypes { get; set; }
    public DbSet<AccommodationStatus>? AccommodationStatuses { get; set; }
    public DbSet<Accommodation>? Accommodations { get; set; }
    public DbSet<AccommodationItem>? AccommodationItems { get; set; }
    public DbSet<AccommodationRate>? AccommodationRates { get; set; }
    public DbSet<AccommodationSubItem>? AccommodationSubItems { get; set; }
    public DbSet<AccommodationStatusHistory>? AccommodationStatusHistories { get; set; }
    public DbSet<Booking>? Bookings { get; set; }
    public DbSet<BookingDetail>? BookingDetails { get; set; }
    public DbSet<BookingDetailStatus>? BookingDetailStatuses { get; set; }
    public DbSet<CashierReconciliation>? CashierReconciliations { get; set; }
    public DbSet<CleaningLog>? CleaningLogs { get; set; }
    public DbSet<CleaningLogDetail>? CleaningLogDetails { get; set; }
    public DbSet<CleaningTask>? CleaningTasks { get; set; }
    public DbSet<Coupon>? Coupons { get; set; }
    public DbSet<CouponDetail>? CouponDetails { get; set; }
    public DbSet<CreditCardType>? CreditCardTypes { get; set; }
    public DbSet<DiscountRate>? DiscountRates { get; set; }
    public DbSet<DrawerSkim>? DrawerSkims { get; set; }
    public DbSet<ExchangeRate>? ExchangeRates { get; set; }
    public DbSet<MaintenanceLog>? MaintenanceLogs { get; set; }
    public DbSet<MaintenanceLogDetail>? MaintenanceLogDetails { get; set; }
    public DbSet<MaintenanceTask>? MaintenanceTasks { get; set; }
    public DbSet<Membership>? Memberships { get; set; }
    public DbSet<MembershipSale>? MembershipSales { get; set; }
    public DbSet<MembershipType>? MembershipTypes { get; set; }
    public DbSet<Organization>? Organizations { get; set; }
    public DbSet<PaidInOut>? PaidInOuts { get; set; }
    public DbSet<ProblemCategory>? ProblemCategories { get; set; }
    public DbSet<Problem>? Problems { get; set; }
    public DbSet<ReceiptMessagesAndCoupon>? ReceiptMessagesAndCoupons { get; set; }
    public DbSet<SalesSession>? SalesSessions { get; set; }
    public DbSet<SalesType>? SalesTypes { get; set; }
    public DbSet<ShiftSetUp>? ShiftSetUps { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }
    public DbSet<WaitingList>? WaitingLists { get; set; }
    public DbSet<Tax>? Taxes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach (var entityTypes in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityTypes.ClrType
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(decimal));
                foreach (var property in properties)
                {
                    modelBuilder
                        .Entity(entityTypes.Name)
                        .Property(property.Name)
                        .HasConversion<double>();
                }
            }
        }
    }
}
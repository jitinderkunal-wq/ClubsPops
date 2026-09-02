namespace ClubPOS.Core.Entities;

public class SalesSession : BaseEntity
{
    public string? AppUserId { get; set; } 
    public DateTime SessionStartDate { get; set; } 
    public DateTime? SessionEndDate { get; set; }
    public int ShiftNumber { get; set; } 
    public decimal BeginFloat { get; set; }
    public int DrawerNumber { get; set; }
    public DateTime BusinessDay { get; set; }
    public bool IsCurrentlyInUse { get; set; }
    public virtual ICollection<CashierReconciliation>? CashierReconciliations { get; set; }
    public virtual ICollection<DrawerSkim>? DrawerSkims { get; set; }
    // public virtual ICollection<LogIn> LogIns { get; set; }
    public virtual ICollection<MembershipSale>? MembershipSales { get; set; }
    public virtual ICollection<PaidInOut>? PaidInOuts { get; set; }
    public virtual ICollection<Problem>? Problems { get; set; }
}
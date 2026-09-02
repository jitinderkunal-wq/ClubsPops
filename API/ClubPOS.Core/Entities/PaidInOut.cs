namespace ClubPOS.Core.Entities;

public class PaidInOut : BaseEntity
{
    public int SalesSessionId { get; set; }
    public DateTime DateCreated { get; set; }
    public decimal PaidIn { get; set; }
    public string? PaidInComment { get; set; }
    public decimal PaidOut { get; set; }
    public string? PaidOutComment { get; set; }

    public virtual SalesSession? SalesSession { get; set; }
}
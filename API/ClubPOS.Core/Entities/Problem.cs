namespace ClubPOS.Core.Entities;

public class Problem : BaseEntity
{
    public int? BookingId { get; set; }
    public int? MembershipId { get; set; }
    public int? MembershipSaleId { get; set; }
    public int ProblemCategoryId { get; set; }
    public int? SalesSessionId { get; set; }
    public string? ProblemExplaination { get; set; }
    public string? UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public string? NewMemberName { get; set; }

    public virtual Booking? Booking { get; set; }
    public virtual Membership? Membership { get; set; }
    public virtual MembershipSale? MembershipSale { get; set; }
    public virtual ProblemCategory? ProblemCategory { get; set; }
    public virtual SalesSession? SalesSession { get; set; }
}
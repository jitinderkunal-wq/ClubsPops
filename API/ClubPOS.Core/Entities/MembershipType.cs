namespace ClubPOS.Core.Entities;

public class MembershipType : BaseEntity
{
    public string? MembershipTypeName { get; set; }
    public decimal DiscountFactor { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool DepositRequired { get; set; }
    public int DisplayOrder { get; set; }
    public bool CanReserve { get; set; }
    public bool CanTab { get; set; }
    public bool IsActive { get; set; }
    public decimal MemberShipPrice { get; set; }

    public virtual ICollection<Membership>? Memberships { get; set; }
    public virtual ICollection<MembershipSale>? MembershipSales { get; set; }
    public virtual ICollection<WaitingList>? WaitingLists { get; set; }
}
namespace ClubPOS.Core.Entities;

public class MembershipSale : BaseEntity
{
    public int MembershipTypeId { get; set; }
    public int SalesSessionId { get; set; }
    public int? TransactionId { get; set; }
    public decimal MembershipPrice { get; set; }
    public DateTime MembershipSaleDate { get; set; }
    public bool FrontDeskSale { get; set; }
    public decimal Total { get; set; }
    public decimal Tendered { get; set; }
    public decimal Cash { get; set; }
    public decimal Debit { get; set; }
    public decimal Visa { get; set; }
    public decimal MasterCard { get; set; }
    public decimal Amex { get; set; }
    public decimal Discover { get; set; }
    public decimal OtherCreditCard { get; set; }
    public decimal USExchangeRate { get; set; }
    public decimal USDollars { get; set; }
    public decimal EuroExchangeRate { get; set; }
    public decimal EuroDollars { get; set; }
    public string? NewMemberNameReceipt { get; set; }

    public virtual MembershipType? MembershipType { get; set; }
    public virtual SalesSession? SalesSession { get; set; }
    public virtual Transaction? Transaction { get; set; }
    public virtual ICollection<Problem>? Problems { get; set; }
}
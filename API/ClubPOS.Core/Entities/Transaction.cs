namespace ClubPOS.Core.Entities;

public class Transaction : BaseEntity
{
    public int? MembershipId { get; set; }
    public int? SalesTypeId { get; set; }
    public int Sequence { get; set; }
    public string? UserId { get; set; }
    public int NumOfGuests { get; set; }
    public DateTime TransactionStartTime { get; set; }
    public DateTime? TransactionCheckInTime { get; set; }
    public DateTime? TransacationCheckOutTime { get; set; }
    public decimal TotalDiscounts { get; set; }
    public decimal TotalDeposits { get; set; }
    public decimal TotalReturnedDeposits { get; set; }
    public decimal TotalNonReturnedDeposits { get; set; }
    public decimal TotalCoupons { get; set; }
    public decimal TotalRefunds { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Taxes { get; set; }
    public decimal GrandTotal { get; set; }
    public decimal AmountTendered { get; set; }
    public decimal Change { get; set; }
    public decimal TotalTransfer { get; set; }
    public decimal TotalVoids { get; set; }
    public decimal TotalCancelled { get; set; }
    public decimal StoredOrder { get; set; }
    public int? StoredOrderTypeId { get; set; }
    public bool IsVoid { get; set; }
    public decimal Cash { get; set; }
    public decimal Debit { get; set; }
    public decimal Amex { get; set; }
    public decimal MasterCard { get; set; }
    public decimal Visa { get; set; }
    public decimal DiscoveryCard { get; set; }
    public decimal UnionPay { get; set; }
    public decimal Diners { get; set; }
    public decimal TotalUSDollars { get; set; }
    public decimal USExchangePaid { get; set; }
    public decimal USExchangeReceived { get; set; }
    public DateTime? TransactionTimeCheckedOut { get; set; }
    public decimal TotalMemberDiscounts { get; set; }
    public decimal TotalRateDiscounts { get; set; }
    public decimal TotalPromos { get; set; }
    public decimal TotalOtherDiscounts { get; set; }
    public decimal RateTotal { get; set; }
    public decimal TotalMembershipSales { get; set; }

    public virtual Membership? Membership { get; set; }
    public virtual SalesType? SalesType { get; set; }
    public virtual ICollection<Booking>? Bookings { get; set; }
    public virtual ICollection<MembershipSale>? MembershipSales { get; set; }
    public virtual ICollection<WaitingList>? WaitingLists { get; set; }
}
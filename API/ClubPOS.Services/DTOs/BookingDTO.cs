namespace ClubPOS.Services.DTOs;

public class BookingDTO
{
    public int TransactionId { get; set; }
    public DateTime? TimeCreated { get; set; }
    public DateTime CheckInTime { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TotalDeposits { get; set; }
    public decimal Refunded { get; set; }
    public decimal Total { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal Tendered { get; set; }
    public decimal Change { get; set; }
    public decimal Cash { get; set; }
    public decimal Debit { get; set; }
    public decimal Amex { get; set; }
    public decimal MasterCard { get; set; }
    public decimal Visa { get; set; }
    public decimal DiscoveryCard { get; set; }
    public decimal UnionPay { get; set; }
    public decimal Diners { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TaxRate { get; set; }
    public string? Comment { get; set; }
    public string? RateDetails { get; set; }
    public decimal TotalTaxes { get; set; }
    public DateTime? TimeCheckedOut { get; set; }
    public DateTime? WakeUpCall { get; set; }
    public decimal? NonRefundedDeposits { get; set; }
    public string? RefundComment { get; set; }
    public decimal RateTotal { get; set; }
    public decimal RateDiscount { get; set; }
    public decimal MemberDiscount { get; set; }
    public decimal OtherDiscountDollar { get; set; }
    public string? OtherDiscountReason { get; set; }
    public decimal Rate { get; set; }
    public decimal CouponsDiscount { get; set; }
    public decimal PromoDiscount { get; set; }
    public DateTime? TimeCompleted { get; set; }
    public string? CheckedOutByEmployee { get; set; }
    public string? NewCreditCardNumber { get; set; }
    public DateTime? NewCardExpiryDate { get; set; }
    public int ShiftBookedIn { get; set; }
    public int ShiftBookedOut { get; set; }
    public string? ReceiptMessage1 { get; set; }
    public string? ReceiptMessage2 { get; set; }
    public string? ReceiptCouponText { get; set; }
    public string? Disclaimer { get; set; }
    public decimal USDollars { get; set; }
    public decimal EuroDollars { get; set; }
    public decimal USExchangeRate { get; set; }
    public decimal EuroExchangeRate { get; set; }
    public decimal MembershipSales { get; set; }
    public int? CheckInLoginId { get; set; }
    public int? CheckOutLoginId { get; set; }
    public decimal NetAmount { get; set; }
    // public decimal TotalSundrySale { get; set; }
    public virtual ICollection<BookingDetailDTO>? BookingDetails { get; set; }
}
namespace ClubPOS.Services.DTOs;

public class BookingCreateDTO
{
    public int AccommodationId { get; set; }
    public int AccommodationRateId { get; set; }
    public int Hours { get; set; }
    public decimal TotalPrice { get; set; }
    public int OrganizationId { get; set; }
    public string? UserId { get; set; }
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
    public decimal UsExchangeReceived { get; set; }
    public decimal Change { get; set; }
    public string? Comment { get; set; }
    public decimal? NonRefundedDeposits { get; set; }
    public decimal OtherDiscountDollar { get; set; }
    public string? OtherDiscountReason { get; set; }
    public int? CouponId { get; set; }
    public int? PromopassId { get; set; }
    public int? DiscountRateId { get; set; }
    public int? MembershipId { get; set; }
    public decimal CouponsDiscount { get; set; }
    public decimal PromoDiscount { get; set; }
    public string? CheckedOutByEmployee { get; set; }
    public int ShiftBookedIn { get; set; }
    public int ShiftBookedOut { get; set; }
    public decimal USDollars { get; set; }
    public decimal EuroDollars { get; set; }
}
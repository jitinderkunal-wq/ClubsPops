namespace ClubPOS.Services.DTOs;

public class BookingDetailDTO
{
    public int BookingId { get; set; }
    public int AccommodationId { get; set; }
    public int? AccommodationItemId { get; set; }
    public int? AccommodationRateId { get; set; }
    public int? BookingDetailStatusId { get; set; }
    public int? CouponId { get; set; }
    public int? PromopassId { get; set; }
    // public int? DiscountRateId { get; set; }
    public int? MembershipId { get; set; }
    public int HoursOfStay { get; set; }
    public int Qty { get; set; }
    public decimal Price { get; set; }
    public bool HasDeposit { get; set; }
    // public decimal DiscountFactor { get; set; }
    // public decimal? NonMemberDiscount { get; set; }
    // public decimal PriceDiscount { get; set; }
    public int Returned { get; set; }
    public int Transfered { get; set; }
    // public decimal MemberDiscountDetail { get; set; }
    public decimal AccommodationCouponDiscount { get; set; }
    public decimal AccommodationPromopassDiscount { get; set; }
    public decimal TransferDepositDetail { get; set; }
    public decimal TransferOut { get; set; }
    public decimal TransferDepositsReturned { get; set; }
    public decimal AccommodationOtherDiscount { get; set; }
    public decimal AccommodationOtherDisPercent { get; set; }
    public DateTime? CheckOutTimeRequested { get; set; }
    public bool ChargableDeposit { get; set; }
    public decimal DepositTransRefund { get; set; }
    public bool IsKeyRefundedOnly { get; set; }
}
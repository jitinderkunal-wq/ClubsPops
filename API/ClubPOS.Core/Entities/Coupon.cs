namespace ClubPOS.Core.Entities;

public class Coupon : BaseEntity
{
    public int? AccommodationTypeId { get; set; }
    public DateTime DateCreated { get; set; }
    public string? CouponName { get; set; }
    public string? Description { get; set; }
    public int CouponNumber { get; set; }
    public decimal FlatRate { get; set; }
    public decimal DiscountDollars { get; set; }
    public decimal DiscountPercent { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsExclusive { get; set; }
    public bool IsActive { get; set; }
    // public int CouponPosition { get; set; }
    // public int BackColour { get; set; }
    // public int ForeColour { get; set; }
    // public string? FontFamility { get; set; }
    // public float FontSize { get; set; }
    public bool IsCoupon { get; set; }
    public bool ReturnOnCheckOut { get; set; }

    public virtual AccommodationType? AccommodationType { get; set; }
    public virtual ICollection<BookingDetail>? BookingDetailCoupons { get; set; }
    public virtual ICollection<BookingDetail>? BookingDetailPromopasses { get; set; }
    public virtual ICollection<CouponDetail>? CouponDetails { get; set; }
}
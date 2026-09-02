namespace ClubPOS.Core.Entities;

public class CouponDetail : BaseEntity
{
    public int CouponId { get; set; }
    public int AccommodationRateId { get; set; }

    public virtual Coupon? Coupon { get; set; }
    public virtual AccommodationRate? AccommodationRate { get; set; }
}
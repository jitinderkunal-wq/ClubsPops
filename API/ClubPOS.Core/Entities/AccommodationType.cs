namespace ClubPOS.Core.Entities;

public class AccommodationType : BaseEntity
{
    public string? TypeName { get; set; }
    public int MaxOccupants { get; set; }
    public int CompatibilityValue { get; set; }

    public virtual ICollection<Accommodation>? Accommodations { get; set; }
    public virtual ICollection<AccommodationRate>? AccommodationRates { get; set; }
    public virtual ICollection<AccommodationSubItem>? AccommodationSubItems { get; set; }
    public virtual ICollection<Coupon>? Coupons { get; set; }
    public virtual ICollection<DiscountRate>? DiscountRates { get; set; }
    public virtual ICollection<WaitingList>? WaitingLists { get; set; }
}
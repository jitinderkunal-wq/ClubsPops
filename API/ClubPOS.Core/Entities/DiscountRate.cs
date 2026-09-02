namespace ClubPOS.Core.Entities;

public class DiscountRate : BaseEntity
{
    public int AccommodationTypeId { get; set; }
    public int? Hours { get; set; }
    public string? RateName { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool Optional { get; set; }
    public bool IsActive { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountFlatRate { get; set; }

    public virtual AccommodationType AccommodationType { get; set; } = null!;
    // public virtual ICollection<Availability> Availabilities { get; set; }
    public virtual ICollection<BookingDetail>? BookingDetails { get; set; }
}
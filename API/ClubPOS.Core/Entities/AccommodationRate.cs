namespace ClubPOS.Core.Entities;

public class AccommodationRate : BaseEntity
{
    public int AccommodationTypeId { get; set; }
    public int Hours { get; set; }
    public string? RateName { get; set; }
    public decimal Price { get; set; }
    public string? StartDayOfTheWeek { get; set; }
    public DateTime? RateStartTime { get; set; }
    public string? EndDayOfWeek { get; set; }
    public DateTime? RateEndTime { get; set; }
    public int? Tax1 { get; set; }
    public int? Tax2 { get; set; }
    public int? Tax3 { get; set; }
    public int? Tax4 { get; set; }
    public int? Tax5 { get; set; }
    public bool IsStandardRate { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsActive { get; set; }

    public virtual AccommodationType? AccommodationType { get; set; }
    public virtual Tax? Tax1Navigation { get; set; }
    public virtual Tax? Tax2Navigation { get; set; }
    public virtual Tax? Tax3Navigation { get; set; }
    public virtual Tax? Tax4Navigation { get; set; }
    public virtual Tax? Tax5Navigation { get; set; }

    public virtual ICollection<BookingDetail>? BookingDetails { get; set; }
    // public virtual ICollection<Availability> Availabilities { get; set; }
    public virtual ICollection<CouponDetail>? CouponDetails { get; set; }
}
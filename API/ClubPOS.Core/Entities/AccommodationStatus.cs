namespace ClubPOS.Core.Entities;

public class AccommodationStatus : BaseEntity
{
    public string? StatusName { get; set; }

    public virtual ICollection<Accommodation>? Accommodations { get; set; }
    public virtual ICollection<AccommodationStatusHistory>? AccommodationStatusHistories { get; set; }
}
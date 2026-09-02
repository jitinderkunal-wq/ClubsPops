namespace ClubPOS.Core.Entities;

public class AccommodationStatusHistory : BaseEntity
{
    public int AccommodationId { get; set; }
    public int AccommodationStatusId { get; set; }
    public DateTime StatusChangeDate { get; set; }

    public virtual Accommodation? Accommodation { get; set; }
    public virtual AccommodationStatus? AccommodationStatus { get; set; }
}
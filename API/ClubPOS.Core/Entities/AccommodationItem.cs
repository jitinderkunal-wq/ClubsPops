namespace ClubPOS.Core.Entities;

public class AccommodationItem : BaseEntity
{
    public string? ItemName { get; set; }
    public decimal Price { get; set; }
    public decimal? DepositAmount { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<AccommodationSubItem>? AccommodationSubItems { get; set; }
    public virtual ICollection<BookingDetail>? BookingDetails { get; set; }
}
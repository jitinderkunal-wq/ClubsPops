namespace ClubPOS.Core.Entities;

public class BookingDetailStatus : BaseEntity
{
    public string? BookingDetailStatusName { get; set; }

    public virtual ICollection<BookingDetail>? BookingDetails { get; set; }
}
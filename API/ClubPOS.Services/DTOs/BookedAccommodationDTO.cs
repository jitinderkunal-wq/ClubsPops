namespace ClubPOS.Services.DTOs;

public class BookedAccommodationDTO
{
    public int BookedAccommodationId { get; set; }
    public virtual AccommodationStatusTypeMaintenanceLogWaitingListDTO? BookedAccommodation { get; set; }
    public virtual BookingDTO? Booking { get; set; }
}
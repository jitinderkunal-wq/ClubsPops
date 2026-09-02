namespace ClubPOS.Core.Entities;

public class Accommodation : BaseEntity
{
    public int AccommodationTypeId { get; set; }
    public int? AccommodationStatusId { get; set; }
    public string? Number { get; set; }
    public int Floor { get; set; }
    public bool DeActivate { get; set; }
    public bool RequiresCreditCard { get; set; }
    public bool HasMaintenanceReservation { get; set; }
    public string? EmployeeLocker { get; set; }
    public bool IsGhost { get; set; }
    public bool HasGloryHole { get; set; }

    public virtual AccommodationType? AccommodationType { get; set; }
    public virtual AccommodationStatus? AccommodationStatus { get; set; }
    public virtual ICollection<AccommodationStatusHistory>? AccommodationStatusHistories { get; set; }
    public virtual ICollection<BookingDetail>? BookingDetails { get; set; }
    public virtual ICollection<MaintenanceLog>? MaintenanceLogs { get; set; }
    public virtual ICollection<CleaningLog>? CleaningLogs { get; set; }
    // public virtual ICollection<Reservation> Reservations { get; set; }
    public virtual ICollection<WaitingList>? WaitingListAccommodations { get; set; }
    public virtual ICollection<WaitingList>? WaitingListRoomAssigneds { get; set; }
}
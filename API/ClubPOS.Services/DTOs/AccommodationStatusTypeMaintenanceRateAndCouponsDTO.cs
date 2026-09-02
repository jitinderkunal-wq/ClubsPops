namespace ClubPOS.Services.DTOs;

public class AccommodationStatusTypeMaintenanceRateAndCouponsDTO
{
    public int AccommodationId { get; set; }
    public int AccommodationTypeId { get; set; }
    public string? TypeName { get; set; }
    public int? AccommodationStatusId { get; set; }
    public string? StatusName { get; set; }
    public string? Number { get; set; }
    public int Floor { get; set; }
    public bool DeActivate { get; set; }
    public bool RequiresCreditCard { get; set; }
    public bool HasMaintenanceReservation { get; set; }
    public string? EmployeeLocker { get; set; }
    public bool IsGhost { get; set; }
    public bool HasGloryHole { get; set; }

    public virtual AccommodationRateDTO? AccommodationRate { get; set; }
    public virtual MaintenanceLogDTO? LastMaintenanceLog { get; set; }
    public virtual ICollection<CouponDTO>? Coupons { get; set; }
}
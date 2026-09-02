namespace ClubPOS.Services.DTOs;

public class EditAccommodationMaintenanceDTO
{
    public int AccommodationId { get; set; }
    public int? NewAccommodationStatusId { get; set; }
    public int OrganizationId { get; set; }
    public string? UserId { get; set; }
     public List<int>? MaintenanceTaskIds { get; set; }
    public string? ResolvedComment { get; set; }
    public string? ResolvedUserId { get; set; }
    public bool NeedsCleaning { get; set; }
}
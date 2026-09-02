namespace ClubPOS.Services.DTOs;

public class CreateAccommodationMaintenanceDTO
{
    public int AccommodationId { get; set; }
    public int NewAccommodationStatusId { get; set; }
    public int OrganizationId { get; set; }
    public string? UserId { get; set; }
     public List<int>? MaintenanceTaskIds { get; set; }
    public string? Comment { get; set; }
}
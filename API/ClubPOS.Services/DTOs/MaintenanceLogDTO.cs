namespace ClubPOS.Services.DTOs;

public class MaintenanceLogDTO
{
    public int MaintenanceLogId { get; set; }
    public int AccommodationId { get; set; }
    public int OrganizationId { get; set; }
    public string? UserId { get; set; }
    public DateTime LogDate { get; set; }
    public DateTime? BackInServiceDate { get; set; }
    public string? Comment { get; set; }
    public string? ResolvedComment { get; set; }
    public bool NeedsCleaning { get; set; }
    public string? ResolvedUserId { get; set; }
    public virtual ICollection<MaintenanceLogDetailDTO>? MaintenanceLogDetails { get; set; }
}
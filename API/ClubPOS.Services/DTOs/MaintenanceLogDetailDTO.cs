namespace ClubPOS.Services.DTOs;

public class MaintenanceLogDetailDTO
{
    public int MaintenanceLogDetailId { get; set; }
    public int MaintenanceTaskId { get; set; }
    public string? TaskName { get; set; }
    public bool IsResolved { get; set; }
    public DateTime? DateResolved { get; set; }
}
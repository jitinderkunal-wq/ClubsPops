namespace ClubPOS.Services.DTOs;

public class MaintenanceTaskDTO
{
    public int MaintenanceTaskId { get; set; }
    public int OrganizationId { get; set; }
    public string? UserId { get; set; }
    public string? TaskName { get; set; }
    public bool IsEndUserFixable { get; set; }
    public bool IsActive { get; set; }
}
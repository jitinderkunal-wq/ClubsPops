namespace ClubPOS.Services.DTOs;

public class CleaningTaskDTO
{
    public int CleaningTaskId { get; set; }
    public int OrganizationId { get; set; }
    public string? UserId { get; set; }
    public string? TaskName { get; set; }
    public bool IsActive { get; set; }
}
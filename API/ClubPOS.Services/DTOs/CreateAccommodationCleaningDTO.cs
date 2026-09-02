namespace ClubPOS.Services.DTOs;

public class CreateAccommodationCleaningDTO
{
    public int AccommodationId { get; set; }
    public int NewAccommodationStatusId { get; set; }
    public int OrganizationId { get; set; }
    public string? UserId { get; set; }
     public List<int>? CleaningTaskIds { get; set; }
    public string? Comment { get; set; }
    public bool NeedsMaintenance { get; set; }
}
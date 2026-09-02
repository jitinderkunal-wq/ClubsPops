namespace ClubPOS.Services.DTOs;

public class AccommodationStatusDTO
{
    public int AccommodationStatusId { get; set; }
    public string? StatusName { get; set; }
    public bool isActive { get; set; }
    public string? UserId { get; set; }
    public int OrganizationId { get; set; }
}
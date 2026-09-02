namespace ClubPOS.Services.DTOs;

public class WaitingListCreateDTO
{
    public int AccommodationId { get; set; }
    public int AccommodationTypeId { get; set; }
    public int MembershipTypeId { get; set; }
    public int OrganizationId { get; set; }
    public string? UserId { get; set; }
    public string? Phone { get; set; }
    public string? Comment { get; set; }
}
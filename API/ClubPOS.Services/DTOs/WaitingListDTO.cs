namespace ClubPOS.Services.DTOs;

public class WaitingListDTO
{
    public int AccommodationId { get; set; }
    public int? AccommodationTypeId { get; set; }
    public int? MembershipId { get; set; }
    public int MembershipTypeId { get; set; }
    public int? RoomAssignedId { get; set; }
    public int TransactionId { get; set; }
    public int OrganizationId { get; set; }
    public string? UserId { get; set; }
    public string? Phone { get; set; }
    public string? Comment { get; set; }
    public DateTime? WaitListTimeAdded { get; set; }
    public bool IsActive { get; set; }
}
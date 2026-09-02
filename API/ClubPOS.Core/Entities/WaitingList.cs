namespace ClubPOS.Core.Entities;

public class WaitingList : BaseEntity
{
    public int AccommodationId { get; set; }
    public int? AccommodationTypeId { get; set; }
    public int? MembershipId { get; set; }
    public int MembershipTypeId { get; set; }
    public int? RoomAssignedId { get; set; }
    public int TransactionId { get; set; }
    public string? UserId { get; set; }
    public string? Phone { get; set; }
    public string? Comment { get; set; }
    public DateTime? WaitListTimeAdded { get; set; }
    public bool IsActive { get; set; }

    public virtual Accommodation? Accommodation { get; set; }
    public virtual AccommodationType? AccommodationType { get; set; }
    public virtual Membership? Membership { get; set; }
    public virtual MembershipType? MembershipType { get; set; }
    public virtual Accommodation? RoomAssigned { get; set; }
    public virtual Transaction? Transaction { get; set; }
}
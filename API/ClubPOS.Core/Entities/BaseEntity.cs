namespace ClubPOS.Core.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }

    public string? CreatedByUserId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? EditedByUserId { get; set; }
    public DateTime? EditedDate { get; set; }
    public bool? IsDeleted { get; set; }
    public string? DeletedByUserId { get; set; }
    public DateTime? DeletedDate { get; set; }
}
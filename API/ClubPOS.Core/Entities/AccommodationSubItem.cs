namespace ClubPOS.Core.Entities;

public class AccommodationSubItem
{
    public int AccommodationTypeId { get; set; }
    public int AccommodationItemId { get; set; }
    public int OrganizationId { get; set; }
    public bool IsRequired { get; set; }
    public int MaxQty { get; set; }
    public bool IsActive { get; set; }

    public virtual AccommodationItem? AccommodationItem { get; set; }
    public virtual AccommodationType? AccommodationType { get; set; }
}
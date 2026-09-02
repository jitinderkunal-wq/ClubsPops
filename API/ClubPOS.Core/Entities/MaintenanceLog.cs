namespace ClubPOS.Core.Entities;

public class MaintenanceLog : BaseEntity
{
    public int AccommodationId { get; set; }
    public string? UserId { get; set; }
    public DateTime LogDate { get; set; }
    public DateTime? BackInServiceDate { get; set; }
    public string? Comment { get; set; }
    public string? ResolvedComment { get; set; }
    public bool NeedsCleaning { get; set; }
    public string? ResolvedUserId { get; set; }

    public virtual Accommodation? Accommodation { get; set; }
    public virtual ICollection<MaintenanceLogDetail>? MaintenanceLogDetails { get; set; }
}
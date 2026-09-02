namespace ClubPOS.Core.Entities;

public class CleaningLog : BaseEntity
{
    public int AccommodationId { get; set; }
    public string? UserId { get; set; }
    public DateTime LogDate { get; set; }
    public string? Comment { get; set; }
    public bool NeedsMaintenance { get; set; }

    public virtual Accommodation? Accommodation { get; set; }
    public virtual ICollection<CleaningLogDetail>? CleaningLogDetails { get; set; }
}
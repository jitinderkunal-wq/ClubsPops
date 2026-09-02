namespace ClubPOS.Core.Entities;

public class CleaningTask : BaseEntity
{
    public string? TaskName { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<CleaningLogDetail>? CleaningLogDetails { get; set; }
}
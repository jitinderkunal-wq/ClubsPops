namespace ClubPOS.Core.Entities;

public class CleaningLogDetail : BaseEntity
{
    public int CleaningLogId { get; set; }
    public int CleaningTaskId { get; set; }

    public virtual CleaningLog? CleaningLog { get; set; }
    public virtual CleaningTask? CleaningTask { get; set; }
}
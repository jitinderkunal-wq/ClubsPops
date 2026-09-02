namespace ClubPOS.Core.Entities;

public class MaintenanceTask : BaseEntity
{
    public string? TaskName { get; set; }
    public bool IsEndUserFixable { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<MaintenanceLogDetail>? MaintenanceLogDetails { get; set; }
}
namespace ClubPOS.Core.Entities;

public class MaintenanceLogDetail : BaseEntity
{
    public int MaintenanceLogId { get; set; }
    public int MaintenanceTaskId { get; set; }
    public bool IsResolved { get; set; }
    public DateTime? DateResolved { get; set; }

    public virtual MaintenanceLog? MaintenanceLog { get; set; }
    public virtual MaintenanceTask? MaintenanceTask { get; set; }
}
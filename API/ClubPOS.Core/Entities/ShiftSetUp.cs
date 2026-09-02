namespace ClubPOS.Core.Entities;

public class ShiftSetUp : BaseEntity
{
    public int ShiftNumber { get; set; }
    public DateTime ShiftStart { get; set; }
    public DateTime ShiftEnd { get; set; }
    public bool IsActive { get; set; }
}
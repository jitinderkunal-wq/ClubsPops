namespace ClubPOS.Services.DTOs;

public class ProblemDTO
{
    public int ProblemId { get; set; }
    public int? BookingId { get; set; }
    // public int? MembershipId { get; set; }
    // public int? MembershipSalesId { get; set; }
    public int ProblemCategoryId { get; set; }
    // public int? SalesSessionId { get; set; }
    public string? ProblemExplaination { get; set; }
    public string? UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public string? NewMemberName { get; set; }
}
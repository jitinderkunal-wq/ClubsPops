using Microsoft.AspNetCore.Identity;

namespace ClubPOS.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal PayRate { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime? TerminationDate { get; set; }
    public int? SIN { get; set; }
    public bool IsActive { get; set; }
    public bool IsSalary { get; set; }
    public bool InTrainingMode { get; set; }
    public int OrganizationId { get; set; }

    public UserAddress? UserAddress { get; set; }
}
namespace ClubPOS.Core.Entities;

public class Organization
{
    public int OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? Logo { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? EditedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
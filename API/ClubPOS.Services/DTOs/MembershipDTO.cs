using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;
public class MembershipDTO
{
    public int MembershipId { get; set; }
    public int? CreditCardTypeId { get; set; }
    [Required]
    public int MembershipTypeId { get; set; }
    public int? MembershipNum { get; set; }
    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public string? PostalZipCode { get; set; }
    [Required]
    public DateTime MembershipStartDate { get; set; }
    public DateTime? MembershipEndDate { get; set; }
    public string? CreditCardNumber { get; set; }
    public DateTime? CreditCardExpiry { get; set; }
    public int? Csv { get; set; }
    public bool IsActive { get; set; }
    public string? UserId { get; set; }
    public int OrganizationId { get; set; }
}
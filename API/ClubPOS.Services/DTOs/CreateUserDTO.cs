using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class CreateUserDTO
{
    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal PayRate { get; set; }
    [Required]
    public DateTime HireDate { get; set; }
    public DateTime? TerminationDate { get; set; }
    public int? SIN { get; set; }
    public bool IsActive { get; set; }
    public bool IsSalary { get; set; }
    public bool InTrainingMode { get; set; }
    [Required]
    public int OrganizationId { get; set; }
    public string? Role { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage = "Password must have 1 Uppercase 1 Lowercase, 1 Number, 1 Non-Alphanumeric and atleast 6 characters")]
    public required string Password { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
    public required string ConfirmPassword { get; set; }

    public string? Street { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    public string? Province { get; set; }

    [Required]
    public string? PostalCode { get; set; }

    public string? CellPhone { get; set; }
}
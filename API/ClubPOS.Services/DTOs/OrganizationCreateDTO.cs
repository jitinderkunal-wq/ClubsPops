using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class OrganizationCreateDTO
{
    [Required]
    public string? OrganizationName { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    [Required]
    public string? RegistrationNumber { get; set; }
    public string? Logo { get; set; }
}
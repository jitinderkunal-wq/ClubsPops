using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class AddressDTO
{
    [Required]
    public string? Street { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public string? Province { get; set; }
    [Required]
    public string? PostalCode { get; set; }
    public string? CellPhone { get; set; }
}
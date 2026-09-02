using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class AccommodationTypeDTO
{
    public int AccommodationTypeId { get; set; }
    [Required]
    public string? TypeName { get; set; }
    public int MaxOccupants { get; set; }
    public int CompatibilityValue { get; set; }
    public bool isActive { get; set; }
    public string? UserId { get; set; }
    public int OrganizationId { get; set; }
}
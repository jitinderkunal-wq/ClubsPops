using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class AccommodationCreateDTO
{
    [Required]
    [Range(1, 100)]
    public int AccommodationTypeId { get; set; }
    public int? AccommodationStatusId { get; set; }
    [Required]
    public string? Number { get; set; }
    [Required]
    [Range(1, 10)]
    public int Floor { get; set; }
    public bool DeActivate { get; set; }
    public bool RequiresCreditCard { get; set; }
    public bool HasMaintenanceReservation { get; set; }
    public string? EmployeeLocker { get; set; }
    public bool IsGhost { get; set; }
    public bool HasGloryHole { get; set; }
    public string? UserId { get; set; }
    public int OrganizationId { get; set; }

}
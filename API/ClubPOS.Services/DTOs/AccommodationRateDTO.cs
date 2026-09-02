using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class AccommodationRateDTO
{
    public int AccommodationRateId { get; set; }
    public int AccommodationTypeId { get; set; }
    public int Hours { get; set; }
    [Required]
    public string? RateName { get; set; }
    public decimal Price { get; set; }
    public string? StartDayOfTheWeek { get; set; }
    public DateTime? RateStartTime { get; set; }
    public string? EndDayOfWeek { get; set; }
    public DateTime? RateEndTime { get; set; }
    public int? Tax1 { get; set; }
    public int? Tax2 { get; set; }
    public int? Tax3 { get; set; }
    public int? Tax4 { get; set; }
    public int? Tax5 { get; set; }
    public bool IsStandardRate { get; set; }
    [Required]
    public decimal TotalPrice { get; set; }
    public bool IsActive { get; set; }
    public string? UserId { get; set; }
    public int OrganizationId { get; set; }
}
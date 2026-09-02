using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class CouponDTO
{
    public int CouponId { get; set; }
    public int? AccommodationTypeId { get; set; }
    public DateTime DateCreated { get; set; }
    [Required]
    public string? CouponName { get; set; }
    public string? Description { get; set; }
    public int CouponNumber { get; set; }
    [Required]
    public decimal FlatRate { get; set; }
    [Required]
    public decimal DiscountDollars { get; set; }
    [Required]
    public decimal DiscountPercent { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsExclusive { get; set; }
    public bool IsActive { get; set; }
    public bool IsCoupon { get; set; }
    public bool ReturnOnCheckOut { get; set; }
    public string? UserId { get; set; }
    public int OrganizationId { get; set; }
}
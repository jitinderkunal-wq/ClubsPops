using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class ReceiptMessagesAndCouponDTO
{
    public int ReceiptMessagesAndCouponId { get; set; }
    [Required]
    public string? Disclaimer { get; set; }
    [Required]
    public string? Message1 { get; set; }
    public string? Message2 { get; set; }
    public string? CouponText { get; set; }
    public string? Address { get; set; }
    public string? GSTNumber { get; set; }
    public string? UserId { get; set; }
    public int OrganizationId { get; set; }
}
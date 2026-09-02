namespace ClubPOS.Core.Entities;

public class ReceiptMessagesAndCoupon : BaseEntity
{
    public string? Disclaimer { get; set; }
    public string? Message1 { get; set; }
    public string? Message2 { get; set; }
    public string? CouponText { get; set; }
    public string? Address { get; set; }
    public string? GSTNumber { get; set; }
}
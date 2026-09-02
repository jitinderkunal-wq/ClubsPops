namespace ClubPOS.Services.DTOs;

public class MembershipTypeDTO
{
    public int MembershipTypeId { get; set; }
    public string? MembershipTypeName { get; set; }
    public decimal DiscountFactor { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool DepositRequired { get; set; }
    public int DisplayOrder { get; set; }
    public bool CanReserve { get; set; }
    public bool CanTab { get; set; }
    public bool IsActive { get; set; }
    public decimal MemberShipPrice { get; set; }
    public string? UserId { get; set; }
    public int OrganizationId { get; set; }
}
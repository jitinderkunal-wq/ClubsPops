using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Core.Entities.Identity;

public class UserAddress
{
    [Key]
    public int AddressId { get; set; }
    public string? Street { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public string? Province { get; set; }
    [Required]
    public string? PostalCode { get; set; }
    public string? CellPhone { get; set; }

    public AppUser? AppUser { get; set; }
    [Required]
    public string? AppUserId { get; set; }
}
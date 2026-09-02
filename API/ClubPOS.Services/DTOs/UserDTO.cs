namespace ClubPOS.Services.DTOs;

public class UserDTO
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public int OrganizationId { get; set; }
    public required string Token { get; set; }
    public DateTime Expiration { get; set; }
    public List<string>? Roles { get; set; }
}
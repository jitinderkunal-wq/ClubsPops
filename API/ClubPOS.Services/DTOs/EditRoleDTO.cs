using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class EditRoleDTO
{
    public string? RoleId { get; set; }
    [Required]
    public string? RoleName { get; set; }
}
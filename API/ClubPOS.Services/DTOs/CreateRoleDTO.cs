using System.ComponentModel.DataAnnotations;

namespace ClubPOS.Services.DTOs;

public class CreateRoleDTO
{
    [Required]
    public string? RoleName { get; set; }
}
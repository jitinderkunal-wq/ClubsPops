using ClubPOS.API.Errors;
using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubPOS.API.Controllers;

[Authorize]
public class AdminController : BaseApiController
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // Admin Related Methods
    [HttpGet("{organizationId}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<ActionResult<IReadOnlyList<EditUserDTO>>> GetAdmins(int organizationId) 
    {
        var allUsers = await _userManager.GetUsersInRoleAsync("Admin");
        var allAdmins = await _userManager.Users.Include(x=>x.UserAddress).ToListAsync();
        var adminsToReturn = new List<EditUserDTO>();
        foreach (var user in allUsers)
        {
            var admins = allAdmins.Where(x => x.Id == user.Id && x.OrganizationId == organizationId).ToList();
            adminsToReturn.AddRange(admins.Select(admin => new EditUserDTO
            {
                UserId = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                PayRate = admin.PayRate,
                HireDate = admin.HireDate,
                TerminationDate = admin.TerminationDate,
                SIN = admin.SIN,
                IsActive = admin.IsActive,
                IsSalary = admin.IsSalary,
                InTrainingMode = admin.InTrainingMode,
                OrganizationId = organizationId,
                Email = admin.Email!,
                Street = admin.UserAddress?.Street,
                City = admin.UserAddress?.City,
                Province = admin.UserAddress?.Province,
                PostalCode = admin.UserAddress?.PostalCode,
                CellPhone = admin.UserAddress?.CellPhone,
            }));
        }
        return Ok(adminsToReturn);
    }

    // Roles Related Methods
    [HttpGet("roles")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<ActionResult<IReadOnlyList<RoleDTO>>> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        var rolesToReturn = roles.Select(role => new RoleDTO
        {
            RoleId = role.Id,
            RoleName = role.Name
        });
        return Ok(rolesToReturn);
    }

    [HttpPost("createRole")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<ActionResult<RoleDTO>> CreateRole(CreateRoleDTO createRoleDTO)
    {
        var checkRoleExists = await _roleManager.RoleExistsAsync(createRoleDTO.RoleName!);
        if (checkRoleExists) return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Role already created" } });
        var identityRole = new IdentityRole { Name = createRoleDTO.RoleName };
        var result = await _roleManager.CreateAsync(identityRole);
        if (!result.Succeeded) return BadRequest(new ApiResponse(400));
        var roleToReturn = new RoleDTO
        {
            RoleId = identityRole.Id,
            RoleName = identityRole.Name
        };
        return Ok(roleToReturn);
    }

    [HttpPatch("editRole")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<ActionResult<RoleDTO>> EditRole(EditRoleDTO editRoleDTO)
    {
        var existingRole = await _roleManager.FindByIdAsync(editRoleDTO.RoleId!);
        if (existingRole == null) return NotFound(new ApiResponse(404));
        var checkRoleExists = await _roleManager.RoleExistsAsync(editRoleDTO.RoleName!);
        if (checkRoleExists) return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Role already created" } });
        existingRole.Name = editRoleDTO.RoleName;
        var result = await _roleManager.UpdateAsync(existingRole);
        if (!result.Succeeded) return BadRequest(new ApiResponse(400));
        var roleToReturn = new RoleDTO
        {
            RoleId = existingRole.Id,
            RoleName = existingRole.Name
        };
        return Ok(roleToReturn);
    }

    [HttpPost("assignRole")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AssignRole(AssignUserRoleDTO assignUserRoleDTO)
    {
        var user = await _userManager.FindByIdAsync(assignUserRoleDTO.UserId!);
        if (user == null) return NotFound(new ApiResponse(404));
        var role = await _roleManager.FindByIdAsync(assignUserRoleDTO.RoleId!);
        if (role == null) return NotFound(new ApiResponse(404));
        var result = await _userManager.AddToRoleAsync(user, role.Name!);
        if (!result.Succeeded) return BadRequest(new ApiResponse(400));
        return Ok();
    }
}
using ClubPOS.API.Errors;
using ClubPOS.API.Extensions;
using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Common;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using ClubPOS.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClubPOS.API.Controllers;

[Authorize]
public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly IOrganizationService _organizationService;
    private readonly IAccountService _accountService;

    public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IOrganizationService organizationService, IAccountService accountService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _organizationService = organizationService;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var userRoles = await _userManager.GetRolesAsync(user);
        var userSpecificClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.GivenName, user.FirstName!)
        };
        foreach (var userRole in userRoles)
        {
            userSpecificClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        var token = _tokenService.GenerateJwtToken(userSpecificClaims);
        var organization = await _organizationService.GetOrganizationByIdAsync(user!.OrganizationId);
        if (organization == null) return Unauthorized("Organization not found");
        var userToReturn = new UserDTO
        {
            UserId = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName!,
            OrganizationId = organization.OrganizationId,
            Token = token,
            Expiration = new JwtSecurityToken(token).ValidTo,
            Roles = userRoles.ToList()
        };
        return Ok(userToReturn);
    }

    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IReadOnlyList<EditUserDTO>>> GetUsers()
    {
        var users = await _userManager.Users.Include(u => u.UserAddress).ToListAsync();
        var usersToReturn = new List<EditUserDTO>();
        foreach (var user in users)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(user.OrganizationId);
            if (organization != null && organization.IsActive == true)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var editUserDTO = new EditUserDTO
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PayRate = user.PayRate,
                    HireDate = user.HireDate,
                    TerminationDate = user.TerminationDate,
                    SIN = user.SIN,
                    IsActive = user.IsActive,
                    IsSalary = user.IsSalary,
                    InTrainingMode = user.InTrainingMode,
                    OrganizationId = organization.OrganizationId,
                    Role = userRoles.Count > 0 ? userRoles[0] : null,
                    Email = user.Email!,
                    Street = user.UserAddress?.Street,
                    City = user.UserAddress?.City,
                    Province = user.UserAddress?.Province,
                    PostalCode = user.UserAddress?.PostalCode,
                    CellPhone = user.UserAddress?.CellPhone
                };
                usersToReturn.Add(editUserDTO);
            }
            else return NotFound("Organization is inactive or not found");
        }
        return Ok(usersToReturn);
    }

    private async Task<bool> IsEmailInUseAsync(string email, string excludeUserId = null!)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(email);
        // If a user with the provided email exists and it's not the current user's email, return true
        return userWithSameEmail != null && (excludeUserId == null || userWithSameEmail.Id != excludeUserId);
    }

    [HttpGet("emailexists")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await IsEmailInUseAsync(email);
    }

    [HttpGet("address")]
    public async Task<ActionResult<AddressDTO>?> GetUserAddress()
    {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
        var userAddress = user?.UserAddress;
        if (userAddress == null) return null;
        var addressDTO = new AddressDTO
        {
            Street = userAddress.Street,
            City = userAddress.City,
            Province = userAddress.Province,
            PostalCode = userAddress.PostalCode,
            CellPhone = userAddress.CellPhone
        };
        return Ok(addressDTO);
    }

    [HttpPatch("address")]
    public async Task<ActionResult<AddressDTO>?> UpdateUserAddress(AddressDTO addressDTO)
    {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
        if (user == null)  return BadRequest("User not found");
        user.UserAddress = new UserAddress
        {
            Street = addressDTO.Street,
            City = addressDTO.City,
            Province = addressDTO.Province,
            PostalCode = addressDTO.PostalCode,
            CellPhone = addressDTO.CellPhone,
            AppUserId = user.Id
        };
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest("Problem updating the user");
        var addressToReturn = new AddressDTO
        {
            Street = user.UserAddress.Street,
            City = user.UserAddress.City,
            Province = user.UserAddress.Province,
            PostalCode = user.UserAddress.PostalCode,
            CellPhone = user.UserAddress.CellPhone,
        };
        return Ok(addressToReturn);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(user.OrganizationId);
            if (organization == null) throw new Exception("Organization not found");
            if (!organization.IsActive) throw new Exception("Organization is inactive, For more information please connect to administrater");
            if (!user.IsActive) throw new Exception("User is inactive, For more information please connect to administrater");
            var userRoles = await _userManager.GetRolesAsync(user);
            var userSpecificClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName, user.FirstName!)
            };
            foreach (var userRole in userRoles)
            {
                userSpecificClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = _tokenService.GenerateJwtToken(userSpecificClaims);
            var userToReturn = new UserDTO
            {
                UserId = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName!,
                OrganizationId = organization.OrganizationId,
                Token = token,
                Expiration = new JwtSecurityToken(token).ValidTo,
                Roles = userRoles.ToList()
            };
            return Ok(userToReturn);
        }
        return Unauthorized();
    }

    [HttpPost("register")]
    [Authorize(Roles = "SuperAdmin, Admin, FrontCash")]
    public async Task<ActionResult<UserDTO>> Register(CreateUserDTO createUserDTO)
    {
        if (await IsEmailInUseAsync(createUserDTO.Email)) return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email Address is in use" } });
        var user = new AppUser
        {
            FirstName = createUserDTO.FirstName,
            LastName = createUserDTO.LastName,
            PayRate = createUserDTO.PayRate,
            HireDate = createUserDTO.HireDate,
            TerminationDate = createUserDTO.TerminationDate,
            SIN = createUserDTO.SIN,
            IsActive = createUserDTO.IsActive,
            IsSalary = createUserDTO.IsSalary,
            InTrainingMode = createUserDTO.InTrainingMode,
            OrganizationId = createUserDTO.OrganizationId,
            Email = createUserDTO.Email,
            PhoneNumber = createUserDTO.CellPhone,
            UserName = createUserDTO.Email,
            UserAddress = new UserAddress
            {
                Street = createUserDTO.Street,
                City = createUserDTO.City,
                Province = createUserDTO.Province,
                PostalCode = createUserDTO.PostalCode,
                CellPhone = createUserDTO.CellPhone
            }
        };
        var result = await _userManager.CreateAsync(user, createUserDTO.Password);
        if (!result.Succeeded) return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "User creation failed! Please check user details and try again." } });
        if (createUserDTO.Role != null) await _userManager.AddToRoleAsync(user, createUserDTO.Role);
        var userSpecificClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.GivenName, user.FirstName!)
        };
        var token = _tokenService.GenerateJwtToken(userSpecificClaims);
        var userToReturn = new UserDTO
        {
            UserId = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName!,
            OrganizationId = user!.OrganizationId,
            Token = token,
            Expiration = new JwtSecurityToken(token).ValidTo
        };
        return Ok(userToReturn);
    }

    [HttpPatch("editUser")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<ActionResult<UserDTO>> EditUser(EditUserDTO editUserDTO)
    {
        var user = await _userManager.Users
        .Include(u => u.UserAddress)
        .FirstOrDefaultAsync(u => u.Id == editUserDTO.UserId);
        if (user == null) return NotFound("User not found");
        if (await IsEmailInUseAsync(editUserDTO.Email, user.Id)) return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email Address is in use which is not yours" } });
        user.FirstName = editUserDTO.FirstName;
        user.LastName = editUserDTO.LastName;
        user.PayRate = editUserDTO.PayRate;
        user.HireDate = editUserDTO.HireDate;
        user.TerminationDate = editUserDTO.TerminationDate;
        user.SIN = editUserDTO.SIN;
        user.IsActive = editUserDTO.IsActive;
        user.IsSalary = editUserDTO.IsSalary;
        user.InTrainingMode = editUserDTO.InTrainingMode;
        user.OrganizationId = editUserDTO.OrganizationId;
        user.Email = editUserDTO.Email;
        user.PhoneNumber = editUserDTO.CellPhone;
        user.UserName = editUserDTO.Email;

        // if (!string.IsNullOrWhiteSpace(editUserDTO.NewPassword))                         // Requires two factor authentication
        // {
        //     // Update password if a new password is provided
        //     var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //     var result = await _userManager.ResetPasswordAsync(user, token, editUserDTO.NewPassword);
        //     if (!result.Succeeded)
        //     {
        //         return BadRequest("Problem updating the password");
        //     }
        // }

        // if (!string.IsNullOrWhiteSpace(editUserDTO.NewPassword))                                     // Requires curretntPassword otherwise gives error
        // {
        //     var result = await _userManager.ChangePasswordAsync(user, null!, editUserDTO.NewPassword);
        //     if (!result.Succeeded)
        //     {
        //         return BadRequest("Problem updating the password");
        //     }
        // }

        if (user.UserAddress != null)
        {
            user.UserAddress.Street = editUserDTO.Street;
            user.UserAddress.City = editUserDTO.City;
            user.UserAddress.Province = editUserDTO.Province;
            user.UserAddress.PostalCode = editUserDTO.PostalCode;
            user.UserAddress.CellPhone = editUserDTO.CellPhone;
        }
        else return NotFound("User Address not found");
        // Save the changes
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded) return BadRequest("Problem updating the user");

        // Edit user role
        // if (!string.IsNullOrWhiteSpace(editUserDTO.NewRole)) // Assuming you have a property like NewRole in the DTO
        // {
        //     var existingRoles = await _userManager.GetRolesAsync(user);
        //     foreach (var role in existingRoles)
        //     {
        //         await _userManager.RemoveFromRoleAsync(user, role);
        //     }

        //     await _userManager.AddToRoleAsync(user, editUserDTO.NewRole);
        // }

        if (!string.IsNullOrWhiteSpace(editUserDTO.Role))
        {
            // Remove existing roles first
            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);
            // Add the new role
            await _userManager.AddToRoleAsync(user, editUserDTO.Role);
        }
        var userSpecificClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.GivenName, user.FirstName!)
        };
        var token = _tokenService.GenerateJwtToken(userSpecificClaims);
        var userToReturn = new UserDTO
        {
            UserId = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName!,
            OrganizationId = user!.OrganizationId,
            Token = token,
            Expiration = new JwtSecurityToken(token).ValidTo
        };
        return Ok(userToReturn);
    }


    [HttpGet("shift")]
    public async Task<ActionResult<IReadOnlyList<ShiftDTO>>> GetShifts()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var shifts = await _accountService.GetShiftsAsync(user.OrganizationId);
        if (shifts == null) return BadRequest("Problem getting shifts");
        return Ok(shifts);
    }

    [HttpGet("getActiveSession")]
    public async Task<ActionResult<IReadOnlyList<ShiftSessionDTO>>> GetActiveSession()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null || user.Id == null) return Unauthorized(new ApiResponse(401));
        var activeSession = await _accountService.GetActiveSessionAsync(user.OrganizationId, user.Id);
        if (activeSession == null) return BadRequest("Problem getting active session");
        return Ok(activeSession);
    }

    [HttpPost("startNewSession")]
    public async Task<ActionResult<ShiftSessionDTO>> StartNewSession(ShiftSessionDTO shiftSessionDTO)
    {
        try
        {
            var user = await _userManager.FindByEmailFromClaimsPricipal(User);
            if (user == null || user.Id == null) return Unauthorized(new ApiResponse(401));
            shiftSessionDTO.OrganizationId =user.OrganizationId;
            var newSession = await _accountService.StartNewSessionAsync(shiftSessionDTO);
            if (newSession == null) return BadRequest("Unable to start new session");
            return Ok(newSession);
        }
        catch (Exception)
        {
            throw new Exception("Problem in create new session");
        }
    }

    [HttpGet("startExistingSession/{shiftSessionId}")]
    public async Task<bool> StartExistingSession(int shiftSessionId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return false;
        var existingSession = await _accountService.StartExistingSessionAsync(user.Id, user.OrganizationId, shiftSessionId);
        if (!existingSession) return false;
        return true;
    }

    [HttpGet("setIsCurrentlyInUseFalse")]
    public async Task<bool> SetIsCurrentlyInUseFalse()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null || user.Id == null) return false;
        var salesSession = await _accountService.SetIsCurrentlyInUseFalseAsync(user.Id, user.OrganizationId);
        if (!salesSession) return false;
        return true;
    }

    [HttpPost("endSession")]
    public async Task<ActionResult<DrawerSkimDTO>> EndSession(DrawerSkimDTO drawerSkimDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null || user.Id == null) return Unauthorized(new ApiResponse(401));
        drawerSkimDTO.UserId = user.Id;
        drawerSkimDTO.OrganizationId = user.OrganizationId;
        var endSession = await _accountService.EndSessionAsync(drawerSkimDTO);
        if (endSession == null) return BadRequest("Unable to end session");
        return Ok(endSession);
    }

    [HttpPost("cashDropDrawerSkim")]
    public async Task<ActionResult<ResponseStatus>> CreateCashDropDrawerSkim(CashDropDrawerSkimDTO cashDropDrawerSkimDTO)
    {
        try
        {
            var user = await _userManager.FindByEmailFromClaimsPricipal(User);
            if (user == null || user.Id == null)
            {
                return new ResponseStatus
                {
                    Status = ResponseEnum.Failed,
                    Message = "Unauthorized"
                };
            }
            cashDropDrawerSkimDTO.UserId = user.Id;
            cashDropDrawerSkimDTO.OrganizationId = user.OrganizationId;
            var response = await _accountService.CreateCashDropDrawerSkim(cashDropDrawerSkimDTO);
            return Ok(response);
        }
        catch (Exception)
        {
            throw;
        }
        
    }
}
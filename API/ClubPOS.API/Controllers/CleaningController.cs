using ClubPOS.API.Errors;
using ClubPOS.API.Extensions;
using ClubPOS.Core.Entities.Common;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClubPOS.API.Controllers;

[Authorize]
public class CleaningController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICleaningService _cleaningService;
    public CleaningController(UserManager<AppUser> userManager, ICleaningService cleaningService)
    {
        _userManager = userManager;
        _cleaningService = cleaningService;
    }

    [Authorize(Roles = "FrontCash, Cleaner, Admin")]
    [HttpGet("cleaningTasks")]
    public async Task<ActionResult<IReadOnlyList<CleaningTaskDTO>>> GetCleaningTasks()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }

        var cleaningTasks = await _cleaningService.GetCleaningTasksAsync(user.OrganizationId);

        return Ok(cleaningTasks);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("cleaningTask")]
    public async Task<ActionResult<CleaningTaskDTO>> CreateCleaningTask(CleaningTaskDTO cleaningTaskDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        cleaningTaskDTO.OrganizationId = user.OrganizationId;
        cleaningTaskDTO.UserId = user.Id;

        var createdCleaningTaskDTO = await _cleaningService.CreateCleaningTaskAsync(cleaningTaskDTO);

        if (createdCleaningTaskDTO == null)
        {
            return BadRequest("Problem Creating CleaningTask");
        }

        return Ok(createdCleaningTaskDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("cleaningTask")]
    public async Task<ActionResult<bool>> EditCleaningTask(CleaningTaskDTO cleaningTaskDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        cleaningTaskDTO.OrganizationId = user.OrganizationId;
        cleaningTaskDTO.UserId = user.Id;
        
        var editedCleaningTaskDTO = await _cleaningService.EditCleaningTaskAsync(cleaningTaskDTO);

        if (!editedCleaningTaskDTO)
        {
            return BadRequest("Problem Updating CleaningTask");
        }

        return Ok(editedCleaningTaskDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("cleaningTask/{cleaningTaskId}")]
    public async Task<ActionResult<bool>> DeleteCleaningTask(int cleaningTaskId)
    {
        var isCleaningTaskDeleted = await _cleaningService.DeleteCleaningTaskAsync(cleaningTaskId);

        return Ok(isCleaningTaskDeleted);
    }

    [Authorize(Roles = "FrontCash, Cleaner")]
    [HttpPost("cleaningLog")]
    public async Task<ActionResult<ResponseStatus>> CreateCleaningLog(CreateAccommodationCleaningDTO createAccommodationCleaningDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Error,
                Message = "Unauthorized",
            };
        }
        createAccommodationCleaningDTO.OrganizationId = user.OrganizationId;
        createAccommodationCleaningDTO.UserId = user.Id;

        var result = await _cleaningService.CreateCleaningLogAsync(createAccommodationCleaningDTO);

        return Ok(result);
    }
}
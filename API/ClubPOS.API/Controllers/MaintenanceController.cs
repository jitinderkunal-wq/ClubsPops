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
public class MaintenanceController : BaseApiController
{
    private readonly IMaintenanceService _maintenanceService;
    private readonly UserManager<AppUser> _userManager;
    public MaintenanceController(UserManager<AppUser> userManager, IMaintenanceService maintenanceService)
    {
        _userManager = userManager;
        _maintenanceService = maintenanceService;
    }

    [Authorize(Roles = "FrontCash, Cleaner, Admin")]
    [HttpGet("maintenanceTasks")]
    public async Task<ActionResult<IReadOnlyList<MaintenanceTaskDTO>>> GetMaintenanceTasks()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }

        var maintenanceTasks = await _maintenanceService.GetMaintenanceTasksAsync(user.OrganizationId);

        return Ok(maintenanceTasks);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("maintenanceTask")]
    public async Task<ActionResult<MaintenanceTaskDTO>> CreateMaintenanceTask(MaintenanceTaskDTO maintenanceTaskDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        maintenanceTaskDTO.OrganizationId = user.OrganizationId;
        maintenanceTaskDTO.UserId = user.Id;

        var createdMaintenanceTaskDTO = await _maintenanceService.CreateMaintenanceTaskAsync(maintenanceTaskDTO);

        if (createdMaintenanceTaskDTO == null)
        {
            return BadRequest("Problem Creating MaintenanceTask");
        }

        return Ok(createdMaintenanceTaskDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("maintenanceTask")]
    public async Task<ActionResult<bool>> EditMaintenanceTask(MaintenanceTaskDTO maintenanceTaskDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        maintenanceTaskDTO.OrganizationId = user.OrganizationId;
        maintenanceTaskDTO.UserId = user.Id;

        var editedMaintenanceTaskDTO = await _maintenanceService.EditMaintenanceTaskAsync(maintenanceTaskDTO);

        if (!editedMaintenanceTaskDTO)
        {
            return BadRequest("Problem Updating MaintenanceTask");
        }

        return Ok(editedMaintenanceTaskDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("maintenanceTask/{maintenanceTaskId}")]
    public async Task<ActionResult<bool>> DeleteMaintenanceTask(int maintenanceTaskId)
    {
        var isMaintenanceTaskDeleted = await _maintenanceService.DeleteMaintenanceTaskAsync(maintenanceTaskId);

        return Ok(isMaintenanceTaskDeleted);
    }



    [Authorize(Roles = "FrontCash, Cleaner, Admin")]
    [HttpGet("maintenanceLogs")]
    public async Task<ActionResult<IReadOnlyList<MaintenanceLogDTO>>> GetMaintenanceLogs()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }

        var maintenanceTasks = await _maintenanceService.GetMaintenanceLogsAsync(user.OrganizationId);

        return Ok(maintenanceTasks);
    }

    [Authorize(Roles = "FrontCash, Admin")]
    [HttpGet("maintenanceLogs/{accommodationId}")]
    public async Task<ActionResult<IReadOnlyList<MaintenanceLogDTO>>> GetMaintenanceLog(int accommodationId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }

        var maintenanceTasks = await _maintenanceService.GetMaintenanceLogsForSelectedAccommodationAsync(accommodationId, user.OrganizationId);

        return Ok(maintenanceTasks);
    }

    [Authorize(Roles = "FrontCash")]
    [HttpPost("createMaintenanceLog")]
    public async Task<ActionResult<ChangeResult>> CreateMaintenanceLog(CreateAccommodationMaintenanceDTO createAccommodationMaintenanceDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        createAccommodationMaintenanceDTO.OrganizationId = user.OrganizationId;
        createAccommodationMaintenanceDTO.UserId = user.Id;

        var result = await _maintenanceService.CreateMaintenanceLogAsync(createAccommodationMaintenanceDTO);

        return result switch
        {
            ChangeResult.Success => (ActionResult<ChangeResult>)Ok(new ApiResponse(200)),
            ChangeResult.NotFound => (ActionResult<ChangeResult>)NotFound(new ApiResponse(404)),
            ChangeResult.Failure => (ActionResult<ChangeResult>)BadRequest(new ApiResponse(400)),
            _ => (ActionResult<ChangeResult>)Unauthorized(new ApiResponse(401)),
        };
    }

    [Authorize(Roles = "FrontCash, Cleaner")]
    [HttpPatch("editMaintenanceLog")]
    public async Task<ActionResult<ResponseStatus>> EditMaintenanceLog(EditAccommodationMaintenanceDTO editAccommodationMaintenanceDTO)
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
        editAccommodationMaintenanceDTO.OrganizationId = user.OrganizationId;
        editAccommodationMaintenanceDTO.UserId = user.Id;
        var result = await _maintenanceService.EditMaintenanceLogAsync(editAccommodationMaintenanceDTO);
        return Ok(result);
    }
}
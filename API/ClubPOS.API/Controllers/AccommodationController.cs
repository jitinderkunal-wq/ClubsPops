using ClubPOS.API.Errors;
using ClubPOS.API.Extensions;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClubPOS.API.Controllers;

[Authorize]
public class AccommodationController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IAccommodationService _accommodationService;
    public AccommodationController(UserManager<AppUser> userManager, IAccommodationService accommodationService)
    {
        _userManager = userManager;
        _accommodationService = accommodationService;
    }

    [Authorize(Roles = "FrontCash")]
    [HttpGet("{accommodationId}")]
    public async Task<ActionResult<AccommodationStatusTypeMaintenanceRateAndCouponsDTO>> GetAccommodationById(int accommodationId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var accommodation = await _accommodationService.GetAccommodationByIdAsync(user.OrganizationId, accommodationId);
        if (accommodation == null) return NotFound(new ApiResponse(404));
        return Ok(accommodation);
    }

    // [Authorize(Roles = "FrontCash, Cleaner, Admin")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AccommodationStatusTypeMaintenanceLogWaitingListDTO>>> GetAccommodations(int? accommodationTypeId, int? accommodationStatusId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var accommodations = await _accommodationService.GetAccommodationsAsync(user.OrganizationId, accommodationTypeId, accommodationStatusId);
        return Ok(accommodations);
    }

    [Authorize(Roles = "FrontCash")]
    [HttpGet("bookedAccommodations")]
    public async Task<ActionResult<IReadOnlyList<AccommodationStatusTypeMaintenanceLogWaitingListDTO>>> GetAllBookedAccommodations()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var accommodations = await _accommodationService.GetAllBookedAccommodationsAsync(user.OrganizationId);
        return Ok(accommodations);
    }

    [Authorize(Roles = "FrontCash")]
    [HttpGet("bookedAccommodationsDetail/{accommodationId}")]
    public async Task<ActionResult<IReadOnlyList<BookedAccommodationDetailDTO>>> GetBookedAccommodationDetail(int accommodationId) 
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var accommodations = await _accommodationService.GetBookedAccommodationDetailAsync(user.OrganizationId, accommodationId);
        return Ok(accommodations);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<AccommodationStatusTypeMaintenanceLogWaitingListDTO>> CreateAccommodation(AccommodationCreateDTO accommodationCreateDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        accommodationCreateDTO.UserId = user.Id; 
        accommodationCreateDTO.OrganizationId = user.OrganizationId;
        var accommodationTypeStatusDTO = await _accommodationService.CreateAccommodationAsync(accommodationCreateDTO);
        if (accommodationTypeStatusDTO == null) return BadRequest("Problem Creating Accommodation");
        return Ok(accommodationTypeStatusDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch]
    public async Task<ActionResult<bool>> EditAccommodation(AccommodationDTO accommodationDTO)
    {
        if (accommodationDTO == null) return BadRequest("Invalid accommodation data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        accommodationDTO.UserId = user.Id; 
        accommodationDTO.OrganizationId = user.OrganizationId;
        var isAccommodationUpdated = await _accommodationService.EditAccommodationAsync(accommodationDTO);
        if (!isAccommodationUpdated) return NotFound("Accommodation you wanted to update was not found");
        return Ok(isAccommodationUpdated);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{accommodationId}")]
    public async Task<ActionResult<bool>> DeleteAccommodation(int accommodationId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var isAccommodationDeleted = await _accommodationService.DeleteAccommodationAsync(user.Id, user.OrganizationId, accommodationId);
        return Ok(isAccommodationDeleted);
    }

    // Accommodation Types
    [Authorize(Roles = "FrontCash, Admin")]
    [HttpGet("getAccommodationTypes")]
    public async Task<ActionResult<IReadOnlyList<AccommodationTypeDTO>>> GetAccommodationTypes()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var accommodationTypes = await _accommodationService.GetAccommodationTypesAsync(user.OrganizationId);
        return Ok(accommodationTypes);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("createAccommodationType")]
    public async Task<ActionResult<AccommodationTypeDTO>> CreateAccommodationType(AccommodationTypeDTO accommodationTypeDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        accommodationTypeDTO.UserId = user.Id;
        accommodationTypeDTO.OrganizationId = user.OrganizationId;
        var accommodationTypes = await _accommodationService.CreateAccommodationTypeAsync(accommodationTypeDTO);
        return Ok(accommodationTypes);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("updateAccommodationType")]
    public async Task<ActionResult<AccommodationTypeDTO>> UpdateAccommodationType(AccommodationTypeDTO accommodationTypeDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        accommodationTypeDTO.UserId = user.Id;
        accommodationTypeDTO.OrganizationId = user.OrganizationId;
        var accommodationTypes = await _accommodationService.UpdateAccommodationTypeAsync(accommodationTypeDTO);
        return Ok(accommodationTypes);
    }

    [Authorize(Roles = "FrontCash, Admin")]
    [HttpGet("getAccommodationStatuses")]
    public async Task<ActionResult<IReadOnlyList<AccommodationStatusDTO>>> GetAccommodationStatuses()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var accommodationStatuses = await _accommodationService.GetAccommodationStatusesAsync(user.OrganizationId);
        return Ok(accommodationStatuses);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("createAccommodationStatus")]
    public async Task<ActionResult<AccommodationStatusDTO>> CreateAccommodationStatus(AccommodationStatusDTO accommodationStatusDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        accommodationStatusDTO.UserId = user.Id;
        accommodationStatusDTO.OrganizationId = user.OrganizationId;
        var accommodationTypes = await _accommodationService.CreateAccommodationStatusAsync(accommodationStatusDTO);
        return Ok(accommodationTypes);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("updateAccommodationStatus")]
    public async Task<ActionResult<AccommodationStatusDTO>> UpdateAccommodationStatus(AccommodationStatusDTO accommodationStatusDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        accommodationStatusDTO.UserId = user.Id;
        accommodationStatusDTO.OrganizationId = user.OrganizationId;
        var accommodationTypes = await _accommodationService.UpdateAccommodationStatusAsync(accommodationStatusDTO);
        return Ok(accommodationTypes);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("accommodationStatus")]
    public async Task<ActionResult<bool>> EditAccommodationStatus(int accommodationId, int newAccommodationStatusId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var isAccommodationStatusUpdated = await _accommodationService.EditAccommodationStatusAsync(user.Id, user.OrganizationId, accommodationId, newAccommodationStatusId); 
        if (!isAccommodationStatusUpdated) return NotFound("Accommodation status was not updated");
        return Ok(isAccommodationStatusUpdated);
    }

    [Authorize(Roles = "FrontCash")]
    [HttpGet("overDueAccommodations")]
    public async Task<ActionResult<IReadOnlyList<BookedAccommodationDTO>>> GetAllOverDueAccommodations()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var accommodations = await _accommodationService.GetOverDueAccommodationsAsync(user.OrganizationId);
        return Ok(accommodations);
    }
}
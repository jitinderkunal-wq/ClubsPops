using ClubPOS.API.Errors;
using ClubPOS.API.Extensions;
using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Common;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClubPOS.API.Controllers;

[Authorize(Roles = "FrontCash")]
public class BookingController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IBookingService _bookingService;
    public BookingController(UserManager<AppUser> userManager, IBookingService bookingService)
    {
        _userManager = userManager;
        _bookingService = bookingService;
    }

    [HttpGet("activeWaitingLists")]
    public async Task<ActionResult<IReadOnlyList<WaitingListDTO>>> GetActiveWaitingLists()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var activeWaitingLists = await _bookingService.GetActiveWaitingListsAsync(user.OrganizationId);
        return Ok(activeWaitingLists);
    }

    [HttpGet("getMembershipLists")]
    public async Task<ActionResult<IReadOnlyList<MembershipListDTO>>> GetMembershipLists()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var membershipLists = await _bookingService.GetMembershipListsAsync(user.OrganizationId);
        return Ok(membershipLists);
    }

    [HttpPost("addToWaiting")]
    public async Task<ActionResult<ResponseStatus>> AddToWaiting(WaitingListCreateDTO waitingListCreateDTO)
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
        waitingListCreateDTO.OrganizationId = user.OrganizationId;
        waitingListCreateDTO.UserId = user.Id;
        var result = await _bookingService.AddToWaitingAsync(waitingListCreateDTO);
        return Ok(result);
    }

    [HttpGet("deleteWaiting/{accommodationId}")]
    public async Task<ActionResult<bool>> DeleteWaiting(int accommodationId)
    {
        try
        {
            if (accommodationId == 0) return false;
            var user = await _userManager.FindByEmailFromClaimsPricipal(User);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _bookingService.DeleteWaitingAsync(user.OrganizationId, accommodationId);
            if (result == false) return BadRequest("Problem while Adding to waiting list Or This Accomodation is already Exist");
            return Ok(result);
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add in Waiting List", ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<BookingDTO>> CheckIn(BookingCreateDTO bookingCreateDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        bookingCreateDTO.OrganizationId = user.OrganizationId;
        bookingCreateDTO.UserId = user.Id;
        var bookingDTO = await _bookingService.CheckInAccommodationAsync(bookingCreateDTO);
        if (bookingDTO == null) return BadRequest("Problem Booking Accommodation");
        return Ok(bookingDTO);
    }

    [HttpPatch("{accommodationId}")]
    public async Task<ActionResult<bool>> CheckOut(int accommodationId)
    {
        var checkOutResult = await _bookingService.CheckOutAccommodationAsync(accommodationId);
        if (checkOutResult == false) return BadRequest("Problem CheckingOut Accommodation");
        return Ok(checkOutResult);
    }

    [HttpGet("refundAmount/{accommodationId}")]
    public async Task<ActionResult<bool>> RefundAmount(int accommodationId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var bookedAccommodation = await _bookingService.RefundAmountAsync(user.OrganizationId, accommodationId);
        if (bookedAccommodation == false) return BadRequest("Problem Refund Amount");
        return bookedAccommodation;
    }

    [HttpGet("{accommodationId}")]
    public async Task<ActionResult<bool>> RenewBooking(int accommodationId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var bookedAccommodation = await _bookingService.RenewAccommodationAsync(user.OrganizationId, accommodationId);
        if (bookedAccommodation == false) return BadRequest("Problem Booking Accommodation");
        return bookedAccommodation;
    }

    [HttpGet("transferBooking/{bookedAccommodationId}")]
    public async Task<ActionResult<AccommodationRateDTO>> TransferBooking(int bookedAccommodationId) 
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var transferBooking = await _bookingService.TransferAccommodationAsync(user.OrganizationId, bookedAccommodationId);
        if(transferBooking == null) return BadRequest("Problem Booking Accommodation");
        return Ok(transferBooking);  
    }
}
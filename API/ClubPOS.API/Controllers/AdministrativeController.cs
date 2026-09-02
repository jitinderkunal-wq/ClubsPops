using ClubPOS.API.Errors;
using ClubPOS.API.Extensions;
using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using ClubPOS.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubPOS.API.Controllers;

[Authorize(Roles = "Admin")]
public class AdministrativeController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IAdministrativeService _administrativeService;
    public AdministrativeController(UserManager<AppUser> userManager, IAdministrativeService administrativeService)
    {
        _userManager = userManager;
        _administrativeService = administrativeService;
        _userManager = userManager;
    }

    [HttpGet("creditCardTypes")]
    public async Task<ActionResult<IReadOnlyList<MembershipTypeDTO>>> GetCreditCardTypes()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var creditCardTypes = await _administrativeService.GetCreditCardTypesAsync(user.OrganizationId);
        return Ok(creditCardTypes);
    }


    // Coupons
    [HttpGet("coupons")]
    public async Task<ActionResult<IReadOnlyList<CouponDTO>>> GetCoupons()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var coupons = await _administrativeService.GetCouponsAsync(user.OrganizationId);
        return Ok(coupons);
    }

    [HttpPost("coupon")]
    public async Task<ActionResult<CouponDTO>> CreateCoupon(CouponDTO couponDTO)
    {
        if (couponDTO == null) return BadRequest("Invalid Coupon data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        couponDTO.UserId = user.Id;
        couponDTO.OrganizationId = user.OrganizationId;
        var createdCouponDTO = await _administrativeService.CreateCouponAsync(couponDTO);
        if (createdCouponDTO == null) return BadRequest("Problem Creating Coupon");
        return Ok(createdCouponDTO);
    }

    [HttpPatch("coupon")]
    public async Task<ActionResult<bool>> EditCoupon(CouponDTO couponDTO)
    {
        if (couponDTO == null) return BadRequest("Invalid Coupon data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        couponDTO.UserId = user.Id;
        couponDTO.OrganizationId = user.OrganizationId;
        var isCouponUpdated = await _administrativeService.EditCouponAsync(couponDTO);
        if (!isCouponUpdated) return BadRequest("Problem Editing Coupon");
        return Ok(isCouponUpdated);
    }

    [HttpDelete("coupon/{couponId}")]
    public async Task<ActionResult<bool>> DeleteCoupon(int couponId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var isCouponDeleted = await _administrativeService.DeleteCouponAsync(user.Id, user.OrganizationId, couponId);
        return Ok(isCouponDeleted);
    }


    // Membership Types
    [HttpGet("membershipTypes")]
    public async Task<ActionResult<IReadOnlyList<MembershipTypeDTO>>> GetMembershipTypes()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var membershipTypes = await _administrativeService.GetMembershipTypesAsync(user.OrganizationId);
        return Ok(membershipTypes);
    }

    [HttpPost("membershipType")]
    public async Task<ActionResult<MembershipTypeDTO>> CreateMembershipType(MembershipTypeDTO membershipTypeDTO)
    {
        if (membershipTypeDTO == null) return BadRequest("Invalid Membership Type data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        membershipTypeDTO.UserId = user.Id;
        membershipTypeDTO.OrganizationId = user.OrganizationId;
        var createdMembershipTypeDTO = await _administrativeService.CreateMembershipTypeAsync(membershipTypeDTO);
        if (createdMembershipTypeDTO == null) return BadRequest("Problem Creating Membership Type");
        return Ok(createdMembershipTypeDTO);
    }

    [HttpPatch("membershipType")]
    public async Task<ActionResult<bool>> EditMembershipType(MembershipTypeDTO membershipTypeDTO)
    {
        if (membershipTypeDTO == null) return BadRequest("Invalid Membership Type data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        membershipTypeDTO.UserId = user.Id;
        membershipTypeDTO.OrganizationId = user.OrganizationId;
        var isMembershipTypeUpdated = await _administrativeService.EditMembershipTypeAsync(membershipTypeDTO);
        if (!isMembershipTypeUpdated) return BadRequest("Problem Editing Membership Type");
        return Ok(isMembershipTypeUpdated);
    }

    [HttpDelete("membershipType/{membershipTypeId}")]
    public async Task<ActionResult<bool>> DeleteMembershipType(int membershipTypeId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var isMembershipTypeDeleted = await _administrativeService.DeleteMembershipTypeAsync(user.Id, user.OrganizationId, membershipTypeId);
        return Ok(isMembershipTypeDeleted);
    }


    // Memberships
    [HttpGet("memberships")]
    public async Task<ActionResult<IReadOnlyList<MembershipDTO>>> GetMemberships()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var memberships = await _administrativeService.GetMembershipsAsync(user.OrganizationId);
        return Ok(memberships);
    }

    [HttpPost("membership")]
    public async Task<ActionResult<MembershipDTO>> CreateMembership(MembershipDTO membershipDTO)
    {
        if (membershipDTO == null) return BadRequest("Invalid Membership data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        membershipDTO.UserId = user.Id;
        membershipDTO.OrganizationId = user.OrganizationId;
        var createdMembershipDTO = await _administrativeService.CreateMembershipAsync(membershipDTO);
        if (createdMembershipDTO == null) return BadRequest("Problem Creating Membership");
        return Ok(createdMembershipDTO);
    }

    [HttpPatch("membership")]
    public async Task<ActionResult<bool>> EditMembership(MembershipDTO membershipDTO)
    {
        if (membershipDTO == null) return BadRequest("Invalid Membership data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        membershipDTO.UserId = user.Id;
        membershipDTO.OrganizationId = user.OrganizationId;
        var isMembershipUpdated = await _administrativeService.EditMembershipAsync(membershipDTO);
        if (!isMembershipUpdated) return BadRequest("Problem Editing Membership");
        return Ok(isMembershipUpdated);
    }

    [HttpDelete("membership/{membershipId}")]
    public async Task<ActionResult<bool>> DeleteMembership(int membershipId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var isMembershipDeleted = await _administrativeService.DeleteMembershipAsync(user.Id, user.OrganizationId, membershipId);
        return Ok(isMembershipDeleted);
    }


    // Receipt Messages And Coupon
    [HttpPost("receiptMessagesAndCoupon")]
    public async Task<ActionResult<ReceiptMessagesAndCouponDTO>> CreateReceiptMessagesAndCoupon(ReceiptMessagesAndCouponDTO receiptMessagesAndCouponDTO)
    {
        if (receiptMessagesAndCouponDTO == null) return BadRequest("Invalid ReceiptMessagesAndCoupon data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        receiptMessagesAndCouponDTO.UserId = user.Id;
        receiptMessagesAndCouponDTO.OrganizationId = user.OrganizationId;
        var receiptMessage = await _administrativeService.CreateReceiptMessagesAndCouponAsync(receiptMessagesAndCouponDTO);
        if (receiptMessage == null) return BadRequest("Problem Creating Receipt Messages And Coupon");
        return Ok(receiptMessage);
    }

    [HttpGet("tax/{taxId}")]
    public async Task<ActionResult<TaxDTO>> GetTaxByTaxId(int taxId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var taxToReturn = await _administrativeService.GetTaxByTaxIdAsync(user.OrganizationId, taxId);
        if (taxToReturn == null) return NotFound(new ApiResponse(404));
        return Ok(taxToReturn);
    }

    [HttpGet("taxAndExchangeRates")]
    public async Task<ActionResult<TaxExchangeRateDTO>> GetTaxAndExchangeRates()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var taxExchangeRate = await _administrativeService.GetTaxExchangeRate(user.OrganizationId);
        if (taxExchangeRate == null) return BadRequest("Problem Creating Receipt Messages And Coupon");
        return Ok(taxExchangeRate);
    }

    [HttpPost("updateTaxAndExchangeRates")]
    public async Task<ActionResult<TaxExchangeRateDTO>> UpdateTaxAndExchangeRates(TaxExchangeRateDTO taxExchangeRateDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        taxExchangeRateDTO.UserId = user.Id;
        taxExchangeRateDTO.OrganizationId = user.OrganizationId;
        var taxExchangeRate = await _administrativeService.UpdateTaxExchangeRate(taxExchangeRateDTO);
        if (taxExchangeRate == null) return BadRequest("Problem Creating Receipt Messages And Coupon");
        return Ok(taxExchangeRateDTO);
    }


    // Shift
    [HttpGet("shift")]
    public async Task<ActionResult<IReadOnlyList<ShiftDTO>>> GetShifts()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var shifts = await _administrativeService.GetShiftsAsync(user.OrganizationId);
        if (shifts == null) return BadRequest("Problem getting shifts");
        return Ok(shifts);
    }

    [HttpPost("shift")]
    public async Task<ActionResult<ShiftDTO>> CreateShift(ShiftDTO shiftDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        shiftDTO.UserId = user.Id;
        shiftDTO.OrganizationId = user.OrganizationId;
        var shift = await _administrativeService.CreateShiftAsync(shiftDTO);
        if (shift == null) return BadRequest("Problem Creating shift");
        return Ok(shiftDTO);
    }

    [HttpPatch("shift")]
    public async Task<ActionResult<ShiftDTO>> UpdateShift(ShiftDTO shiftDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        shiftDTO.UserId = user.Id;
        shiftDTO.OrganizationId = user.OrganizationId;
        var shift = await _administrativeService.UpdateShiftAsync(shiftDTO);
        if (shift == null) return BadRequest("Problem Updating shift");
        return Ok(shiftDTO);
    }


    //Accommodation Rate
    [HttpGet("accommodationRates")]
    public async Task<ActionResult<IReadOnlyList<AccommodationRateDTO>>> GetAccommodationRates()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var accommodationRates = await _administrativeService.GetAccommodationRatesAsync(user.OrganizationId);
        return Ok(accommodationRates);
    }

    [HttpPost("accommodationRate")]
    public async Task<ActionResult<AccommodationRateDTO>> CreateAccommodationRate(AccommodationRateDTO accommodationRateDTO)
    {
        if (accommodationRateDTO == null) return BadRequest("Invalid Accommodation Rate data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        accommodationRateDTO.UserId = user.Id;
        accommodationRateDTO.OrganizationId = user.OrganizationId;
        var createdAccommodationRateDTO = await _administrativeService.CreateAccommodationRateAsync(accommodationRateDTO);
        if (createdAccommodationRateDTO == null) return BadRequest("Problem Creating Accommodation Rate");
        return Ok(createdAccommodationRateDTO);
    }

    [HttpPatch("accommodationRate")]
    public async Task<ActionResult<bool>> EditAccommodationRate(AccommodationRateDTO accommodationRateDTO)
    {
        if (accommodationRateDTO == null) return BadRequest("Invalid Accommodation Rate data");
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        accommodationRateDTO.UserId = user.Id;
        accommodationRateDTO.OrganizationId = user.OrganizationId;
        var isAccommodationRateUpdated = await _administrativeService.EditAccommodationRateAsync(accommodationRateDTO);
        if (!isAccommodationRateUpdated) return BadRequest("Problem Editing Accommodation Rate");
        return Ok(isAccommodationRateUpdated);
    }

    [HttpDelete("accommodationRate/{accommodationRateId}")]
    public async Task<ActionResult<bool>> DeleteAccommodationRate(int accommodationRateId)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var isAccommodationRateDeleted = await _administrativeService.DeleteAccommodationRateAsync(user.Id, user.OrganizationId, accommodationRateId);
        return Ok(isAccommodationRateDeleted);
    }

    [HttpGet("getActiveSession")]
    public async Task<ActionResult<IReadOnlyList<ActiveSessionCashReconcilationDTO>>> GetActiveSession() 
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null || user.Id == null) return Unauthorized(new ApiResponse(401));
        var users = await _userManager.Users.Include(u => u.UserAddress).ToListAsync();
        var activeSessions = await _administrativeService.GetActiveSessionAsync(user.OrganizationId, users);
        if (activeSessions == null) return BadRequest("Problem getting active session");
        return Ok(activeSessions);
    }

    [HttpGet("getCashReconcilation")]
    public async Task<ActionResult<CashReconcilationDTO>> GetCashReconcilation()
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        var getCashRec = await _administrativeService.GetCashReconcilation(user.Id, user.OrganizationId);
        return Ok(getCashRec); 
    }

    [HttpPost("createCashReconcilation")]
    public async Task<ActionResult<CashReconcilationDTO>> CreateCashReconcilation(CashReconcilationDTO cashReconcilationDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        cashReconcilationDTO.UserId = user.Id;
        cashReconcilationDTO.OrganizationId = user.OrganizationId;
        var getCashRec = await _administrativeService.CreateCashReconcilation(cashReconcilationDTO);
        return Ok(getCashRec);
    }

    [HttpPost("updateCashReconcilation")]
    public async Task<ActionResult<CashReconcilationDTO>> UpdateCashReconcilation(CashReconcilationDTO cashReconcilationDTO)
    {
        var user = await _userManager.FindByEmailFromClaimsPricipal(User);
        if (user == null) return Unauthorized(new ApiResponse(401));
        cashReconcilationDTO.UserId = user.Id;
        cashReconcilationDTO.OrganizationId = user.OrganizationId;
        var getCashRec = await _administrativeService.UpdateCashReconcilation(cashReconcilationDTO);
        return Ok(getCashRec);
    }
}
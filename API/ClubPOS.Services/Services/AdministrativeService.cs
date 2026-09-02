using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Infrastructure.IRepositories;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using PdfSharp.Charting;

namespace ClubPOS.Services.Services;

public class AdministrativeService : IAdministrativeService
{
    private readonly IUnitOfWork _unitOfWork;
    public AdministrativeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<CreditCardTypeDTO>> GetCreditCardTypesAsync(int organizationId)
    {
        var creditCardTypes = await _unitOfWork.Repository<CreditCardType>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var creditCardTypeDTOs = creditCardTypes.Select(cct => new CreditCardTypeDTO
        {
            CreditCardTypeId = cct.Id,
            CreditCardTypeName = cct.CreditCardTypeName
        }).ToList();
        return creditCardTypeDTOs;
    }


    // Coupon related services
    public async Task<IReadOnlyList<CouponDTO>> GetCouponsAsync(int organizationId)
    {
        var coupons = await _unitOfWork.Repository<Coupon>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.IsActive == true); 
        var couponDTOs = coupons.Select(c => new CouponDTO
        {
            CouponId = c.Id,
            AccommodationTypeId = c.AccommodationTypeId,
            DateCreated = c.DateCreated,
            CouponName = c.CouponName,
            Description = c.Description,
            CouponNumber = c.CouponNumber,
            FlatRate = c.FlatRate,
            DiscountDollars = c.DiscountDollars,
            DiscountPercent = c.DiscountPercent,
            StartDate = c.StartDate,
            ExpiryDate = c.ExpiryDate,
            IsExclusive = c.IsExclusive,
            IsActive = c.IsActive,
            IsCoupon = c.IsCoupon,
            ReturnOnCheckOut = c.ReturnOnCheckOut
        }).ToList();
        return couponDTOs;
    }
    public async Task<CouponDTO> CreateCouponAsync(CouponDTO couponDTO)
    {
        var coupon = new Coupon
        {
            AccommodationTypeId = couponDTO.AccommodationTypeId,
            DateCreated = couponDTO.DateCreated,
            CouponName = couponDTO.CouponName,
            Description = couponDTO.Description,
            CouponNumber = couponDTO.CouponNumber,
            FlatRate = couponDTO.FlatRate,
            DiscountDollars = couponDTO.DiscountDollars,
            DiscountPercent = couponDTO.DiscountPercent,
            StartDate = couponDTO.StartDate,
            ExpiryDate = couponDTO.ExpiryDate,
            IsExclusive = couponDTO.IsExclusive,
            IsActive = couponDTO.IsActive,
            IsCoupon = couponDTO.IsCoupon,
            ReturnOnCheckOut = couponDTO.ReturnOnCheckOut,
            OrganizationId = couponDTO.OrganizationId,
            IsDeleted = false,
            CreatedByUserId = couponDTO.UserId,
            CreatedDate = DateTime.Now
        };
        _unitOfWork.Repository<Coupon>().Add(coupon);
        var result = await _unitOfWork.Complete();
        if (result <= 0) throw new Exception("Failed to create Coupon.");
        couponDTO.CouponId = coupon.Id;
        return couponDTO;
    }
    public async Task<bool> EditCouponAsync(CouponDTO couponDTO)
    {
        if (couponDTO.CouponId <= 0) throw new ArgumentException("Invalid CouponId");
        var existingCoupons = await _unitOfWork.Repository<Coupon>().ListAllWhereAsync(x => x.OrganizationId == couponDTO.OrganizationId && x.Id == couponDTO.CouponId && x.IsDeleted == false && x.IsActive == true);
        var existingCoupon = existingCoupons.FirstOrDefault() ?? throw new Exception("Coupon not found");

        existingCoupon.AccommodationTypeId = couponDTO.AccommodationTypeId;
        existingCoupon.DateCreated = couponDTO.DateCreated;
        existingCoupon.CouponName = couponDTO.CouponName;
        existingCoupon.Description = couponDTO.Description;
        existingCoupon.CouponNumber = couponDTO.CouponNumber;
        existingCoupon.FlatRate = couponDTO.FlatRate;
        existingCoupon.DiscountDollars = couponDTO.DiscountDollars;
        existingCoupon.DiscountPercent = couponDTO.DiscountPercent;
        existingCoupon.StartDate = couponDTO.StartDate;
        existingCoupon.ExpiryDate = couponDTO.ExpiryDate;
        existingCoupon.IsExclusive = couponDTO.IsExclusive;
        existingCoupon.IsActive = couponDTO.IsActive;
        existingCoupon.IsCoupon = couponDTO.IsCoupon;
        existingCoupon.ReturnOnCheckOut = couponDTO.ReturnOnCheckOut;
        existingCoupon.OrganizationId = couponDTO.OrganizationId;
        existingCoupon.IsDeleted = !couponDTO.IsActive;
        existingCoupon.EditedByUserId = couponDTO.UserId;
        existingCoupon.EditedDate = DateTime.Now;
        _unitOfWork.Repository<Coupon>().Update(existingCoupon);
        var result = await _unitOfWork.Complete();
        return result > 0;
    }
    public async Task<bool> DeleteCouponAsync(string userId, int organizationId, int couponId)
    {
        var existingCoupons = await _unitOfWork.Repository<Coupon>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == couponId && x.IsDeleted == false && x.IsActive == true);
        var existingCoupon = existingCoupons .FirstOrDefault() ?? throw new ArgumentException("Coupon not found");
        existingCoupon.IsActive = false;
        existingCoupon.IsDeleted = true;
        existingCoupon.DeletedByUserId = userId;
        existingCoupon.DeletedDate = DateTime.Now;
        _unitOfWork.Repository<Coupon>().Update(existingCoupon);
        var result = await _unitOfWork.Complete();
        return result > 0;
    }


    // Membership Type related services
    public async Task<IReadOnlyList<MembershipTypeDTO>> GetMembershipTypesAsync(int organizationId)
    {
        var membershipTypes = await _unitOfWork.Repository<MembershipType>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.IsActive == true);
        var membershipTypeDTOs = membershipTypes.Select(mt => new MembershipTypeDTO
        {
            MembershipTypeId = mt.Id,
            MembershipTypeName = mt.MembershipTypeName,
            DiscountFactor = mt.DiscountFactor,
            DiscountAmount = mt.DiscountAmount,
            DepositRequired = mt.DepositRequired,
            DisplayOrder = mt.DisplayOrder,
            CanReserve = mt.CanReserve,
            CanTab = mt.CanTab,
            IsActive = mt.IsActive,
            MemberShipPrice = mt.MemberShipPrice,
        }).ToList();
        return membershipTypeDTOs;
    }
    public async Task<MembershipTypeDTO> CreateMembershipTypeAsync(MembershipTypeDTO membershipTypeDTO)
    {
        var membershipType = new MembershipType
        {
            MembershipTypeName = membershipTypeDTO.MembershipTypeName,
            DiscountFactor = membershipTypeDTO.DiscountFactor,
            DiscountAmount = membershipTypeDTO.DiscountAmount,
            DepositRequired = membershipTypeDTO.DepositRequired,
            DisplayOrder = membershipTypeDTO.DisplayOrder,
            CanReserve = membershipTypeDTO.CanReserve,
            CanTab = membershipTypeDTO.CanTab,
            IsActive = membershipTypeDTO.IsActive,
            MemberShipPrice = membershipTypeDTO.MemberShipPrice,
            OrganizationId = membershipTypeDTO.OrganizationId,
            IsDeleted = false,
            CreatedByUserId = membershipTypeDTO.UserId,
            CreatedDate = DateTime.Now
        };
        _unitOfWork.Repository<MembershipType>().Add(membershipType);
        var result = await _unitOfWork.Complete();
        if (result <= 0) throw new Exception("Failed to create MembershipType.");
        membershipTypeDTO.MembershipTypeId = membershipType.Id;
        return membershipTypeDTO;
    }
    public async Task<bool> EditMembershipTypeAsync(MembershipTypeDTO membershipTypeDTO)
    {
        if (membershipTypeDTO.MembershipTypeId <= 0) throw new ArgumentException("Invalid MembershipTypeId");
        var existingMembershipTypes = await _unitOfWork.Repository<MembershipType>().ListAllWhereAsync(x => x.OrganizationId == membershipTypeDTO.OrganizationId && x.Id == membershipTypeDTO.MembershipTypeId && x.IsDeleted == false && x.IsActive == true);
        var existingMembershipType = existingMembershipTypes.FirstOrDefault() ?? throw new Exception("MembershipType not found");

        existingMembershipType.MembershipTypeName = membershipTypeDTO.MembershipTypeName;
        existingMembershipType.DiscountFactor = membershipTypeDTO.DiscountFactor;
        existingMembershipType.DiscountAmount = membershipTypeDTO.DiscountAmount;
        existingMembershipType.DepositRequired = membershipTypeDTO.DepositRequired;
        existingMembershipType.DisplayOrder = membershipTypeDTO.DisplayOrder;
        existingMembershipType.CanReserve = membershipTypeDTO.CanReserve;
        existingMembershipType.CanTab = membershipTypeDTO.CanTab;
        existingMembershipType.IsActive = membershipTypeDTO.IsActive;
        existingMembershipType.MemberShipPrice = membershipTypeDTO.MemberShipPrice;
        existingMembershipType.OrganizationId = membershipTypeDTO.OrganizationId;
        existingMembershipType.IsDeleted = !membershipTypeDTO.IsActive;
        existingMembershipType.EditedByUserId = membershipTypeDTO.UserId;
        existingMembershipType.EditedDate = DateTime.Now;
        _unitOfWork.Repository<MembershipType>().Update(existingMembershipType);
        var result = await _unitOfWork.Complete();
        return result > 0;
    }
    public async Task<bool> DeleteMembershipTypeAsync(string userId, int organizationId, int membershipTypeId)
    {
        var existingMembershipTypes = await _unitOfWork.Repository<MembershipType>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == membershipTypeId && x.IsDeleted == false && x.IsActive == true);
        var existingMembershipType = existingMembershipTypes.FirstOrDefault() ?? throw new ArgumentException("MembershipType not found");
        existingMembershipType.IsActive = false;
        existingMembershipType.IsDeleted = true;
        existingMembershipType.DeletedByUserId = userId;
        existingMembershipType.DeletedDate = DateTime.Now;
        _unitOfWork.Repository<MembershipType>().Update(existingMembershipType);
        var result = await _unitOfWork.Complete();
        return result > 0;
    }


    // Membership related services
    public async Task<IReadOnlyList<MembershipDTO>> GetMembershipsAsync(int organizationId)
    {
        var memberships = await _unitOfWork.Repository<Membership>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.IsActive == true);
        var membershipDTOs = memberships.Select(membership => new MembershipDTO
        {
            MembershipId = membership.Id,
            CreditCardTypeId = membership.CreditCardTypeId,
            MembershipTypeId = membership.MembershipTypeId,
            MembershipNum = membership.MembershipNum,
            FirstName = membership.FirstName,
            LastName = membership.LastName,
            Address = membership.Address,
            Phone = membership.Phone,
            City = membership.City,
            Province = membership.Province,
            Country = membership.Country,
            PostalZipCode = membership.PostalZipCode,
            MembershipStartDate = membership.MembershipStartDate,
            MembershipEndDate = membership.MembershipEndDate,
            CreditCardNumber = membership.CreditCardNumber,
            CreditCardExpiry = membership.CreditCardExpiry,
            Csv = membership.Csv,
            IsActive = membership.IsActive
        }).ToList();
        return membershipDTOs;
    }
    public async Task<MembershipDTO> CreateMembershipAsync(MembershipDTO membershipDTO)
    {
        var membership = new Membership
        {
            CreditCardTypeId = membershipDTO.CreditCardTypeId,
            MembershipTypeId = membershipDTO.MembershipTypeId,
            MembershipNum = membershipDTO.MembershipNum,
            FirstName = membershipDTO.FirstName,
            LastName = membershipDTO.LastName,
            Address = membershipDTO.Address,
            Phone = membershipDTO.Phone,
            City = membershipDTO.City,
            Province = membershipDTO.Province,
            Country = membershipDTO.Country,
            PostalZipCode = membershipDTO.PostalZipCode,
            MembershipStartDate = membershipDTO.MembershipStartDate,
            MembershipEndDate = membershipDTO.MembershipEndDate,
            CreditCardNumber = membershipDTO.CreditCardNumber,
            CreditCardExpiry = membershipDTO.CreditCardExpiry,
            Csv = membershipDTO.Csv,
            IsActive = membershipDTO.IsActive,
            OrganizationId = membershipDTO.OrganizationId,
            IsDeleted = false,
            CreatedByUserId = membershipDTO.UserId,
            CreatedDate = DateTime.Now
        };
        _unitOfWork.Repository<Membership>().Add(membership);
        var result = await _unitOfWork.Complete();
        if (result <= 0) throw new Exception("Failed to create Membership");
        membershipDTO.MembershipId = membership.Id;
        return membershipDTO;
    }
    public async Task<bool> EditMembershipAsync(MembershipDTO membershipDTO)
    {
        if (membershipDTO.MembershipId <= 0) throw new ArgumentException("Invalid MembershipId");
        var existingMemberships = await _unitOfWork.Repository<Membership>().ListAllWhereAsync(x => x.OrganizationId == membershipDTO.OrganizationId && x.Id == membershipDTO.MembershipId && x.IsDeleted == false && x.IsActive == true);
        var existingMembership = existingMemberships.FirstOrDefault() ?? throw new Exception("Membership not found");

        existingMembership.CreditCardTypeId = membershipDTO.CreditCardTypeId;
        existingMembership.MembershipTypeId = membershipDTO.MembershipTypeId;
        existingMembership.MembershipNum = membershipDTO.MembershipNum;
        existingMembership.FirstName = membershipDTO.FirstName;
        existingMembership.LastName = membershipDTO.LastName;
        existingMembership.Address = membershipDTO.Address;
        existingMembership.Phone = membershipDTO.Phone;
        existingMembership.City = membershipDTO.City;
        existingMembership.Province = membershipDTO.Province;
        existingMembership.Country = membershipDTO.Country;
        existingMembership.PostalZipCode = membershipDTO.PostalZipCode;
        existingMembership.MembershipStartDate = membershipDTO.MembershipStartDate;
        existingMembership.MembershipEndDate = membershipDTO.MembershipEndDate;
        existingMembership.CreditCardNumber = membershipDTO.CreditCardNumber;
        existingMembership.CreditCardExpiry = membershipDTO.CreditCardExpiry;
        existingMembership.Csv = membershipDTO.Csv;
        existingMembership.IsActive = membershipDTO.IsActive;
        existingMembership.OrganizationId = membershipDTO.OrganizationId;
        existingMembership.IsDeleted = !membershipDTO.IsActive;
        existingMembership.EditedByUserId = membershipDTO.UserId;
        existingMembership.EditedDate = DateTime.Now;
        _unitOfWork.Repository<Membership>().Update(existingMembership);
        var result = await _unitOfWork.Complete();
        return result > 0;
    }
    public async Task<bool> DeleteMembershipAsync(string userId, int organizationId, int membershipId)
    {
        var existingMemberships = await _unitOfWork.Repository<Membership>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == membershipId && x.IsDeleted == false && x.IsActive == true);
        var existingMembership = existingMemberships.FirstOrDefault() ?? throw new ArgumentException("Membership not found");
        existingMembership.IsActive = false;
        existingMembership.IsDeleted = true;
        existingMembership.DeletedByUserId = userId;
        existingMembership.DeletedDate = DateTime.Now;
        _unitOfWork.Repository<Membership>().Update(existingMembership);
        var result = await _unitOfWork.Complete();
        return result > 0;
    }


    // ReceiptMessages And Coupon related services
    public async Task<ReceiptMessagesAndCouponDTO> CreateReceiptMessagesAndCouponAsync(ReceiptMessagesAndCouponDTO receiptMessagesAndCouponDTO)
    {
        var receiptMessagesAndCoupon = new ReceiptMessagesAndCoupon
        {
            Disclaimer = receiptMessagesAndCouponDTO.Disclaimer,
            Message1 = receiptMessagesAndCouponDTO.Message1,
            Message2 = receiptMessagesAndCouponDTO.Message2,
            CouponText = receiptMessagesAndCouponDTO.CouponText,
            Address = receiptMessagesAndCouponDTO.Address,
            GSTNumber = receiptMessagesAndCouponDTO.GSTNumber,
            OrganizationId = receiptMessagesAndCouponDTO.OrganizationId,
            IsDeleted = false,
            CreatedByUserId = receiptMessagesAndCouponDTO.UserId,
            CreatedDate = DateTime.Now
        };
        // Save the Receipt Messages And Coupon to the database
        _unitOfWork.Repository<ReceiptMessagesAndCoupon>().Add(receiptMessagesAndCoupon);
        var result = await _unitOfWork.Complete();
        if (result <= 0) throw new Exception("Failed to create ReceiptMessagesAndCoupon.");
        receiptMessagesAndCouponDTO.ReceiptMessagesAndCouponId = receiptMessagesAndCoupon.Id;
        return receiptMessagesAndCouponDTO;
    }
    public async Task<TaxDTO> GetTaxByTaxIdAsync(int organizationId, int taxId)
    {
        var taxLists = await _unitOfWork.Repository<Tax>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.Id == taxId);
        var tax = taxLists.FirstOrDefault();
        if (tax == null) return null!;
        var taxDTO = new TaxDTO
        {
            TaxId = tax.Id,
            TaxName = tax.TaxName,
            TaxRate = tax.TaxRate
        };
        return taxDTO;
    }
    public async Task<TaxExchangeRateDTO> GetTaxExchangeRate(int organizationId)
    {
        var taxs = await _unitOfWork.Repository<Tax>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var gst = taxs.FirstOrDefault(x => x.TaxName == "GST");
        var hst = taxs.FirstOrDefault(x => x.TaxName == "HST");
        var exchangeRate = await _unitOfWork.Repository<ExchangeRate>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var usExchange = exchangeRate.FirstOrDefault(x => x.RateName == "US Exchange Rate");
        var euroExchange = exchangeRate.FirstOrDefault(x => x.RateName == "Euro Exchange Rate");
        var taxExchangeRate = new TaxExchangeRateDTO
        {
            Gst = gst!.TaxRate,
            Hst = hst!.TaxRate,
            UsExchange = usExchange!.Rate,
            EuroExchange = euroExchange!.Rate
        };
        return taxExchangeRate;
    }
    public async Task<TaxExchangeRateDTO> UpdateTaxExchangeRate(TaxExchangeRateDTO taxExchangeRateDTO)
    {
        var taxs = await _unitOfWork.Repository<Tax>().ListAllWhereAsync(x => x.OrganizationId == taxExchangeRateDTO.OrganizationId && x.IsDeleted == false);
        var gst = taxs.FirstOrDefault(x => x.TaxName == "GST") ?? throw new Exception("GST not found");
        var hst = taxs.FirstOrDefault(x => x.TaxName == "HST") ?? throw new Exception("HST not found");
        var tax = taxs.FirstOrDefault() ?? throw new Exception("Tax not found");
        gst.TaxRate = taxExchangeRateDTO.Gst;
        hst.TaxRate = taxExchangeRateDTO.Hst;
        tax.OrganizationId = taxExchangeRateDTO.OrganizationId;
        tax.IsDeleted = true;
        tax.EditedByUserId = taxExchangeRateDTO.UserId;
        tax.EditedDate = DateTime.Now;
        _unitOfWork.Repository<Tax>().Update(gst);
        _unitOfWork.Repository<Tax>().Update(hst);
        await _unitOfWork.Complete();

        var exchangeRates = await _unitOfWork.Repository<ExchangeRate>().ListAllWhereAsync(x => x.OrganizationId == taxExchangeRateDTO.OrganizationId && x.IsDeleted == false);
        var usExchange = exchangeRates.FirstOrDefault(x => x.RateName == "US Exchange Rate") ?? throw new Exception("US Exchange Rate not found");
        var euroExchange = exchangeRates.FirstOrDefault(x => x.RateName == "Euro Exchange Rate") ?? throw new Exception("Euro Exchange Rate not found");
        var exchangeRate = taxs.FirstOrDefault() ?? throw new Exception("Exchange Rate not found");
        usExchange.Rate = taxExchangeRateDTO.UsExchange;
        euroExchange.Rate = taxExchangeRateDTO.EuroExchange;
        exchangeRate.OrganizationId = taxExchangeRateDTO.OrganizationId;
        exchangeRate.IsDeleted = true;
        exchangeRate.EditedByUserId = taxExchangeRateDTO.UserId;
        exchangeRate.EditedDate = DateTime.Now;
        _unitOfWork.Repository<ExchangeRate>().Update(usExchange);
        _unitOfWork.Repository<ExchangeRate>().Update(euroExchange);
        await _unitOfWork.Complete();
        return taxExchangeRateDTO;
    }


    // Shift Related Services
    public async Task<IReadOnlyList<ShiftDTO>> GetShiftsAsync(int organizationId)
    {
        var shifts = await _unitOfWork.Repository<ShiftSetUp>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.IsActive == true);
        var shiftDTOs = shifts.Select(shift => new ShiftDTO
        {
            ShiftId = shift.Id,
            ShiftNumber = shift.ShiftNumber,
            ShiftStart = shift.ShiftStart.ToString(),
            ShiftEnd = shift.ShiftEnd.ToString(),
            IsActive = shift.IsActive
        }).ToList();
        return shiftDTOs;
    }
    public async Task<ShiftDTO> CreateShiftAsync(ShiftDTO shiftDTO)
    {
        var shifts = await _unitOfWork.Repository<ShiftSetUp>().ListAllWhereAsync(x => x.OrganizationId == shiftDTO.OrganizationId && x.ShiftNumber == shiftDTO.ShiftNumber && x.IsDeleted == false && x.IsActive == true);
        var shift = shifts.FirstOrDefault();
        if (shift != null) throw new Exception("Shift already exist");
        var shiftSetUp = new ShiftSetUp
        {
            ShiftNumber = shiftDTO.ShiftNumber,
            ShiftStart = Convert.ToDateTime(shiftDTO.ShiftStart),
            ShiftEnd = Convert.ToDateTime(shiftDTO.ShiftEnd),
            IsActive = shiftDTO.IsActive,
            OrganizationId = shiftDTO.OrganizationId,
            IsDeleted = false,
            CreatedByUserId = shiftDTO.UserId,
            CreatedDate = DateTime.Now
        };
        // Saving Shift in the database
        _unitOfWork.Repository<ShiftSetUp>().Add(shiftSetUp);
        var result = await _unitOfWork.Complete();
        if (result <= 0) throw new Exception("Failed to create Shift");
        return shiftDTO;
    }
    public async Task<ShiftDTO> UpdateShiftAsync(ShiftDTO shiftDTO)
    {
        if (shiftDTO.ShiftId <= 0) throw new ArgumentException("Invalid ShiftId");
        var existingShifts = await _unitOfWork.Repository<ShiftSetUp>().ListAllWhereAsync(x => x.OrganizationId == shiftDTO.OrganizationId && x.Id == shiftDTO.ShiftId && x.IsDeleted == false && x.IsActive == true);
        var existingShift = existingShifts.FirstOrDefault() ?? throw new Exception("Shift not found");
        existingShift.Id = shiftDTO.ShiftId;
        existingShift.ShiftNumber = shiftDTO.ShiftNumber;
        existingShift.ShiftStart = Convert.ToDateTime(shiftDTO.ShiftStart);
        existingShift.ShiftEnd = Convert.ToDateTime(shiftDTO.ShiftEnd);
        existingShift.IsActive = shiftDTO.IsActive;
        existingShift.OrganizationId = shiftDTO.OrganizationId;
        existingShift.IsDeleted = !shiftDTO.IsActive;
        existingShift.EditedByUserId = shiftDTO.UserId;
        existingShift.EditedDate = DateTime.Now;
        // Updating Shift in the database
        _unitOfWork.Repository<ShiftSetUp>().Update(existingShift);
        var result = await _unitOfWork.Complete();
        if (result <= 0) throw new Exception("Failed to update Shift");
        return shiftDTO;
    }


    // AccommodationRate Related Services
    public async Task<IReadOnlyList<AccommodationRateDTO>> GetAccommodationRatesAsync(int organizationId)
    {
        var accommodationRates = await _unitOfWork.Repository<AccommodationRate>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.IsActive == true);
        var accommodationRateDTOs = accommodationRates.Select(accommodationRate => new AccommodationRateDTO
        {
            AccommodationRateId = accommodationRate.Id,
            AccommodationTypeId = accommodationRate.AccommodationTypeId,
            Hours = accommodationRate.Hours,
            RateName = accommodationRate.RateName,
            Price = accommodationRate.Price,
            StartDayOfTheWeek = accommodationRate.StartDayOfTheWeek,
            RateStartTime = accommodationRate.RateStartTime,
            EndDayOfWeek = accommodationRate.EndDayOfWeek,
            RateEndTime = accommodationRate.RateEndTime,
            Tax1 = accommodationRate.Tax1,
            Tax2 = accommodationRate.Tax2,
            Tax3 = accommodationRate.Tax3,
            Tax4 = accommodationRate.Tax4,
            Tax5 = accommodationRate.Tax5,
            IsStandardRate = accommodationRate.IsStandardRate,
            TotalPrice = accommodationRate.TotalPrice,
            IsActive = accommodationRate.IsActive
        }).ToList();
        return accommodationRateDTOs;
    }
    public async Task<AccommodationRateDTO> CreateAccommodationRateAsync(AccommodationRateDTO accommodationRateDTO)
    {
        var accommodationRate = new AccommodationRate
        {
            AccommodationTypeId = accommodationRateDTO.AccommodationTypeId,
            Hours = accommodationRateDTO.Hours,
            RateName = accommodationRateDTO.RateName,
            Price = accommodationRateDTO.Price,
            StartDayOfTheWeek = accommodationRateDTO.StartDayOfTheWeek,
            RateStartTime = accommodationRateDTO.RateStartTime,
            EndDayOfWeek = accommodationRateDTO.EndDayOfWeek,
            RateEndTime = accommodationRateDTO.RateEndTime,
            Tax1 = accommodationRateDTO.Tax1,
            Tax2 = accommodationRateDTO.Tax2,
            Tax3 = accommodationRateDTO.Tax3,
            Tax4 = accommodationRateDTO.Tax4,
            Tax5 = accommodationRateDTO.Tax5,
            IsStandardRate = accommodationRateDTO.IsStandardRate,
            TotalPrice = accommodationRateDTO.TotalPrice,
            IsActive = accommodationRateDTO.IsActive,
            OrganizationId = accommodationRateDTO.OrganizationId,
            IsDeleted = false,
            CreatedByUserId = accommodationRateDTO.UserId,
            CreatedDate = DateTime.Now
        };
        _unitOfWork.Repository<AccommodationRate>().Add(accommodationRate);
        var result = await _unitOfWork.Complete();
        if (result <= 0) throw new Exception("Failed to create AccommodationRate");
        accommodationRateDTO.AccommodationRateId = accommodationRate.Id;
        return accommodationRateDTO;
    }
    public async Task<bool> EditAccommodationRateAsync(AccommodationRateDTO accommodationRateDTO)
    {
        if (accommodationRateDTO.AccommodationRateId <= 0) throw new ArgumentException("Invalid AccommodationRateId");
        var existingAccommodationRates = await _unitOfWork.Repository<AccommodationRate>().ListAllWhereAsync(x => x.OrganizationId == accommodationRateDTO.OrganizationId && x.Id == accommodationRateDTO.AccommodationRateId && x.IsDeleted == false && x.IsActive == true);
        var existingAccommodationRate = existingAccommodationRates .FirstOrDefault() ?? throw new Exception("AccommodationRate not found");

        existingAccommodationRate.AccommodationTypeId = accommodationRateDTO.AccommodationTypeId;
        existingAccommodationRate.Hours = accommodationRateDTO.Hours;
        existingAccommodationRate.RateName = accommodationRateDTO.RateName;
        existingAccommodationRate.Price = accommodationRateDTO.Price;
        existingAccommodationRate.StartDayOfTheWeek = accommodationRateDTO.StartDayOfTheWeek;
        existingAccommodationRate.RateStartTime = accommodationRateDTO.RateStartTime;
        existingAccommodationRate.EndDayOfWeek = accommodationRateDTO.EndDayOfWeek;
        existingAccommodationRate.RateEndTime = accommodationRateDTO.RateEndTime;
        existingAccommodationRate.Tax1 = accommodationRateDTO.Tax1;
        existingAccommodationRate.Tax2 = accommodationRateDTO.Tax2;
        existingAccommodationRate.Tax3 = accommodationRateDTO.Tax3;
        existingAccommodationRate.Tax4 = accommodationRateDTO.Tax4;
        existingAccommodationRate.Tax5 = accommodationRateDTO.Tax5;
        existingAccommodationRate.IsStandardRate = accommodationRateDTO.IsStandardRate;
        existingAccommodationRate.TotalPrice = accommodationRateDTO.TotalPrice;
        existingAccommodationRate.IsActive = accommodationRateDTO.IsActive;
        existingAccommodationRate.OrganizationId = accommodationRateDTO.OrganizationId;
        existingAccommodationRate.IsDeleted = !accommodationRateDTO.IsActive;
        existingAccommodationRate.EditedByUserId = accommodationRateDTO.UserId;
        existingAccommodationRate.EditedDate = DateTime.Now;
        _unitOfWork.Repository<AccommodationRate>().Update(existingAccommodationRate);
        var result = await _unitOfWork.Complete();
        return result > 0;
    }
    public async Task<bool> DeleteAccommodationRateAsync(string userId, int organizationId, int accommodationRateId)
    {
        var existingAccommodationRates = await _unitOfWork.Repository<AccommodationRate>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.Id == accommodationRateId && x.IsDeleted == false && x.IsActive == true);
        var existingAccommodationRate = existingAccommodationRates.FirstOrDefault() ?? throw new ArgumentException("AccommodationRate not found");
        existingAccommodationRate.IsActive = false;
        existingAccommodationRate.IsDeleted = true;
        existingAccommodationRate.DeletedByUserId = userId;
        existingAccommodationRate.DeletedDate = DateTime.Now;
        _unitOfWork.Repository<AccommodationRate>().Update(existingAccommodationRate);
        var result = await _unitOfWork.Complete();
        return result > 0;
    }


    public async Task<IReadOnlyList<ActiveSessionCashReconcilationDTO>> GetActiveSessionAsync(int organizationId, List<AppUser> users)
    {
        var salesSessionsList = await _unitOfWork.Repository<SalesSession>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false && x.SessionEndDate != null && x.IsCurrentlyInUse == false && x.BeginFloat > 0);
        var shiftSessionDTO = new List<ActiveSessionCashReconcilationDTO>();
        foreach (var salesSession in salesSessionsList)
        {
            if(await _unitOfWork.Repository<CashierReconciliation>().ListAllWhereAsync(x => x.SalesSessionId == salesSession.Id) == null) continue;
            var userIds = salesSessionsList.Select(x => x.AppUserId).ToList();
            shiftSessionDTO = salesSessionsList.Select(x => new ActiveSessionCashReconcilationDTO
            {
                SalesSessionId = x.Id,
                UserId = x.AppUserId,
                Name = users.FirstOrDefault(u => u.Id == x.AppUserId)?.FirstName + ' ' + users.FirstOrDefault(u => u.Id == x.AppUserId)?.LastName,
                Shift = x.ShiftNumber,
                BeginFloat = x.BeginFloat,
                DateCreated = x.SessionStartDate,
            }).ToList();
        }
        
        return shiftSessionDTO;
    }

    public async Task<CashReconcilationDTO> GetCashReconcilation(string userId, int organizationId)
    {
        // Acccommodation
        var transections = await _unitOfWork.Repository<Transaction>().ListAllWhereAsync(x => x.UserId == userId && x.OrganizationId == organizationId && x.IsDeleted == false);
        var bookingList = await _unitOfWork.Repository<Booking>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var bookingDetailList = await _unitOfWork.Repository<BookingDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);
        var accommodations = await _unitOfWork.Repository<Accommodation>().ListAllWhereAsync(x => x.OrganizationId == organizationId && x.IsDeleted == false);

        var dayStart = DateTime.Today;
        var dayEnd = dayStart.AddHours(24);

        // Accommodation 

        int lockerQuantity = 0;
        decimal lockerAccomodationTotal = 0;

        int singleRoomQuantity = 0;
        decimal singleRoomAccomodationTotal = 0;

        int singleRoomTVQuantity = 0;
        decimal singleRoomTVAccomodationTotal = 0;

        int doubleRoomTVQuantity = 0;
        decimal doubleRoomTVAccomodationTotal = 0;

        // Transfer 

        int lockerTransferQuantity = 0;
        decimal lockerAccomodationTransferTotal = 0;

        int singleRoomTransferQuantity = 0;
        decimal singleRoomAccomodationTransferTotal = 0;

        int singleRoomTVTransferQuantity = 0;
        decimal singleRoomTVAccomodationTransferTotal = 0;

        int doubleRoomTVTransferQuantity = 0;
        decimal doubleRoomTVAccomodationTransferTotal = 0;

        // Polled Declaration

        decimal accommodationSalesPolled = 0;
        decimal refundAccommodationPolled = 0;
        decimal voidedAccommodationsPolled = 0;
        decimal rateDiscountsPolled = 0;
        decimal couponDiscountsPolled = 0;
        decimal promoDiscountsPolled = 0;
        decimal otherDiscountsRolled = 0;
        decimal memberDiscountsPolled = 0;
        decimal totalAccommodationSalesPolled = 0;

        decimal depositsCollectedPolled = 0;
        decimal depositsReturnedPolled = 0;
        decimal membershipSalesPolled = 0;
        decimal otherSalesPolled = 0;
        decimal barSalesPolled = 0;
        decimal totalFinalSalesPolled = 0;

        decimal debitPolled = 0;
        decimal masterCardPolled = 0;
        decimal visaPolled = 0;
        decimal amexPolled = 0;
        decimal discoverPolled = 0;
        decimal otherCreditCardPolled = 0;
        decimal totalDebitCreditPolled = 0;

        decimal eosDebitCreditTotalPolled = 0;

        decimal totalUsdollarsPolled = 0;
        decimal totalEuroDollarsPolled = 0;

        decimal finalCashDepositPolled = 0;
        decimal paidInsPolled = 0;
        decimal receiptsPaidOutsPolled = 0;
        decimal cashDropsTotalPolled = 0;
        decimal cashOverShortPolled = 0;

        //Deposits
        decimal refundedDeposits = 0;
        decimal forfittedDeposist = 0;
        decimal totalCollected = 0;



        foreach (var item in transections)
        {
            // Accomodation Calculations
            var booking = bookingList.FirstOrDefault(x => x.TransactionId == item.Id && x.TimeCreated >= dayStart && x.TimeCreated <= dayEnd);
            if (booking == null) { continue; }
            var bookingDetail = bookingDetailList.Where(x => x.BookingId == booking.Id && x.Transfered == 0).FirstOrDefault();
            if (bookingDetail != null)
            {
                var accommodation = accommodations.Where(x => x.Id == bookingDetail.AccommodationId).FirstOrDefault();
                if (accommodation == null) { continue; }
                switch (accommodation.AccommodationTypeId)
                {
                    case 1:
                        lockerQuantity += 1;
                        lockerAccomodationTotal += bookingDetail.Price;
                        break;
                    case 2:
                        singleRoomQuantity += 1;
                        singleRoomAccomodationTotal += bookingDetail.Price;
                        break;
                    case 3:
                        singleRoomTVQuantity += 1;
                        singleRoomTVAccomodationTotal += bookingDetail.Price;
                        break;
                    default:
                        doubleRoomTVQuantity += 1;
                        doubleRoomTVAccomodationTotal += bookingDetail.Price;
                        break;
                }

                // Polled Calculations

                refundAccommodationPolled += booking.Refunded;
                voidedAccommodationsPolled += item.TotalVoids;
                rateDiscountsPolled += booking.RateDiscount;
                couponDiscountsPolled += booking.CouponsDiscount;
                promoDiscountsPolled += booking.PromoDiscount;
                otherDiscountsRolled += booking.OtherDiscountDollar;
                memberDiscountsPolled += booking.MemberDiscount;

                depositsCollectedPolled += booking.TotalDeposits;
                depositsReturnedPolled += bookingDetail.TransferDepositsReturned;
                membershipSalesPolled += booking.MembershipSales;
                otherSalesPolled += 0;
                barSalesPolled += 0;

                debitPolled += booking.Debit;
                masterCardPolled += booking.MasterCard;
                visaPolled += booking.Visa;
                amexPolled += booking.Amex;
                discoverPolled += booking.DiscoveryCard;
                otherCreditCardPolled += 0;

                eosDebitCreditTotalPolled += 0;

                totalUsdollarsPolled += booking.USDollars;
                totalEuroDollarsPolled += booking.EuroDollars;

                finalCashDepositPolled += booking.Cash;
                paidInsPolled += 0;
                receiptsPaidOutsPolled += 0;


            }
            #region Transfer Calculations

            //var transfer = bookingDetailList.Where(x=>x.BookingId == booking.Id && x.Transfered == 1).FirstOrDefault();
            //if(transfer != null) 
            //{
            //    var accommodation = accommodations.Where(x=>x.Id == bookingDetail.AccommodationId).FirstOrDefault();
            //    if(accommodation == null) { continue; }
            //    switch(accommodation.AccommodationTypeId)
            //    {
            //        case 1:
            //            lockerTransferQuantity += 1;
            //            lockerAccomodationTransferTotal += transfer.Price;
            //            break;
            //        case 2:
            //            singleRoomTransferQuantity += 1;
            //            singleRoomAccomodationTransferTotal += transfer.Price;
            //            break;
            //        case 3:
            //            singleRoomTVTransferQuantity += 1;
            //            singleRoomTVAccomodationTransferTotal += transfer.Price;
            //            break;
            //        default:
            //            doubleRoomTVTransferQuantity += 1;
            //            doubleRoomTVAccomodationTransferTotal += transfer.Price;
            //            break;
            //    }
            //}
            #endregion
        }

        //Booking Polled
        accommodationSalesPolled = lockerAccomodationTotal + singleRoomAccomodationTotal + singleRoomTVAccomodationTotal + doubleRoomTVAccomodationTotal;
        totalAccommodationSalesPolled = accommodationSalesPolled + refundAccommodationPolled + voidedAccommodationsPolled + rateDiscountsPolled + couponDiscountsPolled + promoDiscountsPolled + otherDiscountsRolled + memberDiscountsPolled;

        totalFinalSalesPolled = depositsCollectedPolled + depositsReturnedPolled + membershipSalesPolled + otherSalesPolled + barSalesPolled + totalAccommodationSalesPolled;

        totalDebitCreditPolled = debitPolled + masterCardPolled + visaPolled + amexPolled + discoverPolled + otherCreditCardPolled;

        cashDropsTotalPolled = finalCashDepositPolled + paidInsPolled + receiptsPaidOutsPolled;
        cashOverShortPolled = totalFinalSalesPolled - totalDebitCreditPolled;

        //Deposits
        refundedDeposits = 0;
        forfittedDeposist = 0;
        totalCollected = 0;

        // Binding values in CashReconcilationDTO
        var cashReconcilation = new CashReconcilationDTO
        {
            AccommodationSalesPolled = accommodationSalesPolled,
            RefundAccommodationPolled = refundAccommodationPolled,
            VoidedAccommodationsPolled = voidedAccommodationsPolled,
            RateDiscountsPolled = rateDiscountsPolled,
            CouponDiscountsPolled = couponDiscountsPolled,
            PromoDiscountsPolled = promoDiscountsPolled,
            OtherDiscountsRolled = otherDiscountsRolled,
            MemberDiscountsPolled = memberDiscountsPolled,
            TotalAccommodationSalesPolled = totalAccommodationSalesPolled,

            DepositsCollectedPolled = depositsCollectedPolled,
            DepositsReturnedPolled = depositsReturnedPolled,
            MembershipSalesPolled = membershipSalesPolled,
            OtherSalesPolled = otherSalesPolled,
            BarSalesPolled = barSalesPolled,
            TotalFinalSalesPolled = totalFinalSalesPolled,

            DebitPolled = debitPolled,
            MasterCardPolled = masterCardPolled,
            VisaPolled = visaPolled,
            AmexPolled = amexPolled,
            DiscoverPolled = discoverPolled,
            OtherCreditCardPolled = otherCreditCardPolled,
            TotalDebitCreditPolled = totalDebitCreditPolled,

            EosDebitCreditTotalPolled = eosDebitCreditTotalPolled,

            TotalUsdollarsPolled = totalUsdollarsPolled,
            TotalEuroDollarsPolled = totalEuroDollarsPolled,

            FinalCashDepositPolled = finalCashDepositPolled,
            PaidInsPolled = paidInsPolled,
            ReceiptsPaidOutsPolled = receiptsPaidOutsPolled,
            CashDropsTotalPolled = cashDropsTotalPolled,
            CashOverShortPolled = cashOverShortPolled,

            //Accommodation
            LockerQuantity = lockerQuantity,
            SingleRoomQuantity = singleRoomQuantity,
            SingleRoomTVQuantity = singleRoomTVQuantity,
            DoubleRoomTVQuantity = doubleRoomTVQuantity,
            LockerAccomodationTotal = lockerAccomodationTotal,
            SingleRoomAccomodationTotal = singleRoomAccomodationTotal,
            SingleRoomTVAccomodationTotal = singleRoomTVAccomodationTotal,
            DoubleRoomTVAccomodationTotal = doubleRoomTVAccomodationTotal,

            //Transfer
            LockerTransferQuantity = lockerTransferQuantity,
            SingleRoomTransferQuantity = singleRoomTransferQuantity,
            SingleRoomTVTransferQuantity = singleRoomTVTransferQuantity,
            DoubleRoomTVTransferQuantity = doubleRoomTVTransferQuantity,
            LockerAccomodationTransferTotal = lockerAccomodationTransferTotal,
            SingleRoomAccomodationTransferTotal = singleRoomAccomodationTransferTotal,
            SingleRoomTVAccomodationTransferTotal = singleRoomTVAccomodationTransferTotal,
            DoubleRoomTVAccomodationTransferTotal = doubleRoomTVAccomodationTransferTotal,

            //Deposits
            RefundedDeposits = refundedDeposits,
            ForfittedDeposist = forfittedDeposist,
            TotalCollected = totalCollected,

        };


        return cashReconcilation;
    }

    public async Task<bool> CreateCashReconcilation(CashReconcilationDTO cashReconcilationDTO)
    {
        var cashierReconciliation = new CashierReconciliation
        {
            SalesSessionId = cashReconcilationDTO.SalesSessionId,
            AccommodationSalesPolled = cashReconcilationDTO.AccommodationSalesPolled,
            VoidedAccommodationsPolled = cashReconcilationDTO.VoidedAccommodationsPolled,
            RateDiscountsPolled = cashReconcilationDTO.RateDiscountsPolled,
            //RefundAccommodationPolled = cashReconcilationDTO.RefundAccommodationPolle,                                  // Comes from Fron-End but not in CashierReconciliation
            CouponDiscountsPolled = cashReconcilationDTO.CouponDiscountsPolled,
            PromodiscouuntsPolled = cashReconcilationDTO.PromoDiscountsPolled,
            OtherDiscountsRolled = cashReconcilationDTO.OtherDiscountsRolled,
            MemberDiscountsPolled = cashReconcilationDTO.MemberDiscountsPolled,
            TotalAccommodationSalesPolled = cashReconcilationDTO.TotalAccommodationSalesPolled,
            DepositsCollectedPolled = cashReconcilationDTO.DepositsCollectedPolled,
            DepositsReturnedPolled = cashReconcilationDTO.DepositsReturnedPolled,
            MembershipSalesPolled = cashReconcilationDTO.MembershipSalesPolled,
            OtherSalesPolled = cashReconcilationDTO.OtherSalesPolled,
            BarSalesPolled = cashReconcilationDTO.BarSalesPolled,
            FinalSalesTotalPolled = cashReconcilationDTO.TotalFinalSalesPolled,
            DebitPolled = cashReconcilationDTO.DebitPolled,
            MasterCardPolled = cashReconcilationDTO.MasterCardPolled,
            Visapolled = cashReconcilationDTO.VisaPolled,
            Amexpolled = cashReconcilationDTO.AmexPolled,
            DiscoverPolled = cashReconcilationDTO.DiscoverPolled,
            OtherCreditCard1Polled = cashReconcilationDTO.OtherCreditCardPolled,
            TotalDebitCreditPolled = cashReconcilationDTO.TotalDebitCreditPolled,
            //EosDebitCreditTotalPolled = cashReconcilationDTO.EosDebitCreditTotalPolle,                                  // Comes from Fron-End but not in CashierReconciliation
            TotalUsdollarsPolled = cashReconcilationDTO.TotalUsdollarsPolled,
            TotalEuroDollarsPolled = cashReconcilationDTO.TotalEuroDollarsPolled,
            FinalCashDepositPolled = cashReconcilationDTO.FinalCashDepositPolled,
            PaidInsPolled = cashReconcilationDTO.PaidInsPolled,
            ReceiptsPaidOutsPolled = cashReconcilationDTO.ReceiptsPaidOutsPolled,
            TotalCashDropsPolled = cashReconcilationDTO.CashDropsTotalPolled,
            CashOverShortPolled = cashReconcilationDTO.CashOverShortPolled,
            OrganizationId = cashReconcilationDTO.OrganizationId,
            CreatedByUserId = cashReconcilationDTO.UserId,
            CreatedDate = DateTime.Now,
            DateCreated = DateTime.Now,
            IsDeleted = false
        };

         _unitOfWork.Repository<CashierReconciliation>().Add(cashierReconciliation);
        var result = await _unitOfWork.Complete();
        if (result <= 0) throw new Exception("Failed to create CashierReconciliation");
        return true;
    }

    public async Task<bool> UpdateCashReconcilation(CashReconcilationDTO cashReconcilationDTO)
    {
        //if (cashReconcilationDTO.CashierReconciliationId <= 0) throw new ArgumentException("Invalid CashierReconciliationId");
        var existingCashierReconciliations = await _unitOfWork.Repository<CashierReconciliation>().ListAllWhereAsync(x => x.OrganizationId == cashReconcilationDTO.OrganizationId && x.Id == cashReconcilationDTO.CashierReconciliationId && x.IsDeleted == false);
        var existingCashierReconciliation = existingCashierReconciliations.FirstOrDefault() ?? throw new Exception("Cashier Reconciliation not found");

        existingCashierReconciliation.AccommodationSalesPolled = cashReconcilationDTO.AccommodationSalesPolled;
        existingCashierReconciliation.VoidedAccommodationsPolled = cashReconcilationDTO.VoidedAccommodationsPolled;
        existingCashierReconciliation.RateDiscountsPolled = cashReconcilationDTO.RateDiscountsPolled;
        //existingCashierReconciliation.RefundAccommodationPolled = cashReconcilationDTO.RefundAccommodationPolled;                                  // Comes from Fron-End but not in CashierReconciliation
        existingCashierReconciliation.CouponDiscountsPolled = cashReconcilationDTO.CouponDiscountsPolled;
        existingCashierReconciliation.PromodiscouuntsPolled = cashReconcilationDTO.PromoDiscountsPolled;
        existingCashierReconciliation.OtherDiscountsRolled = cashReconcilationDTO.OtherDiscountsRolled;
        existingCashierReconciliation.MemberDiscountsPolled = cashReconcilationDTO.MemberDiscountsPolled;
        existingCashierReconciliation.TotalAccommodationSalesPolled = cashReconcilationDTO.TotalAccommodationSalesPolled;
        existingCashierReconciliation.DepositsCollectedPolled = cashReconcilationDTO.DepositsCollectedPolled;
        existingCashierReconciliation.DepositsReturnedPolled = cashReconcilationDTO.DepositsReturnedPolled;
        existingCashierReconciliation.MembershipSalesPolled = cashReconcilationDTO.MembershipSalesPolled;
        existingCashierReconciliation.OtherSalesPolled = cashReconcilationDTO.OtherSalesPolled;
        existingCashierReconciliation.BarSalesPolled = cashReconcilationDTO.BarSalesPolled;
        existingCashierReconciliation.FinalSalesTotalPolled = cashReconcilationDTO.TotalFinalSalesPolled;
        existingCashierReconciliation.DebitPolled = cashReconcilationDTO.DebitPolled;
        existingCashierReconciliation.MasterCardPolled = cashReconcilationDTO.MasterCardPolled;
        existingCashierReconciliation.Visapolled = cashReconcilationDTO.VisaPolled;
        existingCashierReconciliation.Amexpolled = cashReconcilationDTO.AmexPolled;
        existingCashierReconciliation.DiscoverPolled = cashReconcilationDTO.DiscoverPolled;
        existingCashierReconciliation.OtherCreditCard1Polled = cashReconcilationDTO.OtherCreditCardPolled;
        existingCashierReconciliation.TotalDebitCreditPolled = cashReconcilationDTO.TotalDebitCreditPolled;
        //existingCashierReconciliation.EosDebitCreditTotalPolled = cashReconcilationDTO.EosDebitCreditTotalPolled;                                   // Comes from Fron-End but not in CashierReconciliation
        existingCashierReconciliation.TotalUsdollarsPolled = cashReconcilationDTO.TotalUsdollarsPolled;
        existingCashierReconciliation.TotalEuroDollarsPolled = cashReconcilationDTO.TotalEuroDollarsPolled;
        existingCashierReconciliation.FinalCashDepositPolled = cashReconcilationDTO.FinalCashDepositPolled;
        existingCashierReconciliation.PaidInsPolled = cashReconcilationDTO.PaidInsPolled;
        existingCashierReconciliation.ReceiptsPaidOutsPolled = cashReconcilationDTO.ReceiptsPaidOutsPolled;
        existingCashierReconciliation.TotalCashDropsPolled = cashReconcilationDTO.CashDropsTotalPolled;
        existingCashierReconciliation.CashOverShortPolled = cashReconcilationDTO.CashOverShortPolled;

        _unitOfWork.Repository<CashierReconciliation>().Update(existingCashierReconciliation);
        var result = await _unitOfWork.Complete();
        if (result <= 0) throw new Exception("Failed to update CashierReconciliation");
        return true;
    }
}
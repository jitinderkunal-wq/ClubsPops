using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Services.DTOs;

namespace ClubPOS.Services.IServices;

public interface IAdministrativeService
{
    Task<IReadOnlyList<CreditCardTypeDTO>> GetCreditCardTypesAsync(int organizationId);

    Task<IReadOnlyList<CouponDTO>> GetCouponsAsync(int organizationId);
    Task<CouponDTO> CreateCouponAsync(CouponDTO couponDTO);
    Task<bool> EditCouponAsync(CouponDTO couponDTO);
    Task<bool> DeleteCouponAsync(string userId, int organizationId, int CouponId);

    Task<IReadOnlyList<MembershipTypeDTO>> GetMembershipTypesAsync(int organizationId);
    Task<MembershipTypeDTO> CreateMembershipTypeAsync(MembershipTypeDTO membershipTypeDTO);
    Task<bool> EditMembershipTypeAsync(MembershipTypeDTO membershipTypeDTO);
    Task<bool> DeleteMembershipTypeAsync(string userId, int organizationId, int membershipTypeId);

    Task<IReadOnlyList<MembershipDTO>> GetMembershipsAsync(int organizationId);
    Task<MembershipDTO> CreateMembershipAsync(MembershipDTO membershipDTO);
    Task<bool> EditMembershipAsync(MembershipDTO membershipDTO);
    Task<bool> DeleteMembershipAsync(string userId, int organizationId, int membershipId);

    Task<ReceiptMessagesAndCouponDTO> CreateReceiptMessagesAndCouponAsync(ReceiptMessagesAndCouponDTO receiptMessagesAndCouponDTO);

    Task<TaxDTO> GetTaxByTaxIdAsync(int organizationId, int taxId);
    Task<TaxExchangeRateDTO> GetTaxExchangeRate(int organizationId);
    Task<TaxExchangeRateDTO> UpdateTaxExchangeRate(TaxExchangeRateDTO taxExchangeRateDTO);

    Task<IReadOnlyList<ShiftDTO>> GetShiftsAsync(int organizationId);
    Task<ShiftDTO> CreateShiftAsync(ShiftDTO shiftDTO);
    Task<ShiftDTO> UpdateShiftAsync(ShiftDTO shiftDTO);

    Task<IReadOnlyList<AccommodationRateDTO>> GetAccommodationRatesAsync(int organizationId);
    Task<AccommodationRateDTO> CreateAccommodationRateAsync(AccommodationRateDTO accommodationRateDTO);
    Task<bool> EditAccommodationRateAsync(AccommodationRateDTO accommodationRateDTO);
    Task<bool> DeleteAccommodationRateAsync(string userId, int organizationId, int AccommodationRateId);
    Task<CashReconcilationDTO> GetCashReconcilation(string userId, int organizationId);
    Task<bool> CreateCashReconcilation(CashReconcilationDTO cashReconcilationDTO);
    Task<bool> UpdateCashReconcilation(CashReconcilationDTO cashReconcilationDTO);

    Task<IReadOnlyList<ActiveSessionCashReconcilationDTO>> GetActiveSessionAsync(int organizationId, List<AppUser> users);
}
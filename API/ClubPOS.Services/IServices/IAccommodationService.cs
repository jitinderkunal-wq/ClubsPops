using ClubPOS.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ClubPOS.Services.IServices;

public interface IAccommodationService
{
    Task<AccommodationStatusTypeMaintenanceRateAndCouponsDTO?> GetAccommodationByIdAsync(int organizationId, int accommodationId);
    Task<IReadOnlyList<AccommodationStatusTypeMaintenanceLogWaitingListDTO>> GetAccommodationsAsync(int organizationId, int? accommodationTypeId, int? accommodationStatusId);
    Task<IReadOnlyList<BookedAccommodationDTO>> GetAllBookedAccommodationsAsync(int organizationId);
    Task<BookedAccommodationDetailDTO> GetBookedAccommodationDetailAsync(int organizationId, int accommodationId); 
    Task<IReadOnlyList<AccommodationTypeDTO>> GetAccommodationTypesAsync(int organizationId);
    Task<AccommodationTypeDTO> CreateAccommodationTypeAsync(AccommodationTypeDTO accommodationTypeDTO);
    Task<bool> UpdateAccommodationTypeAsync(AccommodationTypeDTO accommodationTypeDTO);
    Task<IReadOnlyList<AccommodationStatusDTO>> GetAccommodationStatusesAsync(int organizationId);
    Task<AccommodationStatusDTO> CreateAccommodationStatusAsync(AccommodationStatusDTO accommodationStatusDTO);
    Task<bool> UpdateAccommodationStatusAsync(AccommodationStatusDTO accommodationStatusDTO);
    Task<bool> EditAccommodationStatusAsync(string userId, int organizationId, int accommodationId, int newAccommodationStatusId);
    Task<AccommodationStatusTypeMaintenanceLogWaitingListDTO> CreateAccommodationAsync(AccommodationCreateDTO accommodationCreateDTO);
    Task<bool> EditAccommodationAsync(AccommodationDTO accommodationDTO);
    Task<bool> DeleteAccommodationAsync(string userId, int organizationId, int accommodationId);
    Task<IReadOnlyList<BookedAccommodationDTO>> GetOverDueAccommodationsAsync(int organizationId);
}
using ClubPOS.Core.Entities.Common;
using ClubPOS.Services.DTOs;

namespace ClubPOS.Services.IServices;

public interface IBookingService
{
    Task<IReadOnlyList<WaitingListDTO>> GetActiveWaitingListsAsync(int organizationId);
    Task<IReadOnlyList<MembershipListDTO>> GetMembershipListsAsync(int organizationId); 
    Task<BookingDTO?> CheckInAccommodationAsync(BookingCreateDTO bookingCreateDTO);
    Task<bool> CheckOutAccommodationAsync(int accommodationId);
    Task<ResponseStatus> AddToWaitingAsync(WaitingListCreateDTO waitingListCreateDTO);
    Task<bool> RefundAmountAsync(int organizationId, int accommodationId);
    Task<bool> RenewAccommodationAsync(int organizationId, int accommodationId);
    Task<bool> DeleteWaitingAsync(int organizationId, int accommodationId);
    Task<BookingDetailDTO> TransferAccommodationAsync(int organizationId, int bookedAccommodationId); 
}
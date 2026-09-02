using ClubPOS.Core.Entities.Common;
using ClubPOS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.IServices
{
    public interface IAccountService
    {
        
        Task<IReadOnlyList<ShiftDTO>> GetShiftsAsync(int organizationId);
        Task<IReadOnlyList<ShiftSessionDTO>> GetActiveSessionAsync(int organizationId, string userId);
        Task<ShiftSessionDTO> StartNewSessionAsync(ShiftSessionDTO shiftSessionDTO);  
        Task<bool> StartExistingSessionAsync(string userId, int organizationId, int shiftSessionId);   
        Task<bool> SetIsCurrentlyInUseFalseAsync(string userId, int organizationId);  
        Task<DrawerSkimDTO> EndSessionAsync(DrawerSkimDTO drawerSkimDTO);
        Task<ResponseStatus> CreateCashDropDrawerSkim(CashDropDrawerSkimDTO cashDropDrawerSkimDTO);
        Task<bool> EndPreviousDayShiftAutomatically();
    }
}

using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Common;
using ClubPOS.Services.DTOs;

namespace ClubPOS.Services.IServices;

public interface IMaintenanceService
{
    Task<IReadOnlyList<MaintenanceTaskDTO>> GetMaintenanceTasksAsync(int organizationId);
    Task<MaintenanceTaskDTO> CreateMaintenanceTaskAsync(MaintenanceTaskDTO maintenanceTaskDTO);
    Task<bool> EditMaintenanceTaskAsync(MaintenanceTaskDTO maintenanceTaskDTO);
    Task<bool> DeleteMaintenanceTaskAsync(int maintenanceTaskId);
    
    Task<IReadOnlyList<MaintenanceLogDTO>> GetMaintenanceLogsAsync(int organizationId);
    Task<IReadOnlyList<MaintenanceLogDTO>> GetMaintenanceLogsForSelectedAccommodationAsync(int accommodationId, int organizationId);
    Task<ChangeResult> CreateMaintenanceLogAsync(CreateAccommodationMaintenanceDTO createAccommodationMaintenanceDTO);
    Task<ResponseStatus> EditMaintenanceLogAsync(EditAccommodationMaintenanceDTO editAccommodationMaintenanceDTO);
}
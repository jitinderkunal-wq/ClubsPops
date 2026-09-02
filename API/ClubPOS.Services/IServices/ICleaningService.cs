using ClubPOS.Core.Entities.Common;
using ClubPOS.Services.DTOs;

namespace ClubPOS.Services.IServices;

public interface ICleaningService
{
    Task<IReadOnlyList<CleaningTaskDTO>> GetCleaningTasksAsync(int organizationId);
    Task<CleaningTaskDTO> CreateCleaningTaskAsync(CleaningTaskDTO cleaningTaskDTO);
    Task<bool> EditCleaningTaskAsync(CleaningTaskDTO cleaningTaskDTO);
    Task<bool> DeleteCleaningTaskAsync(int cleaningTaskId);

    Task<ResponseStatus> CreateCleaningLogAsync(CreateAccommodationCleaningDTO createAccommodationCleaningDTO);
}
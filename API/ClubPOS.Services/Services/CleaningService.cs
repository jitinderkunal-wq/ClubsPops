using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Common;
using ClubPOS.Infrastructure.IRepositories;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;

namespace ClubPOS.Services.Services;

public class CleaningService : ICleaningService
{
    private readonly IUnitOfWork _unitOfWork;
    public CleaningService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<CleaningTaskDTO>> GetCleaningTasksAsync(int organizationId)
    {
        // var cleaningTasks = await _unitOfWork.Repository<CleaningTask>().ListAllAsync();
        var cleaningTasks = await _unitOfWork.Repository<CleaningTask>().ListAllWhereAsync(task => task.OrganizationId == organizationId);

        var cleaningTasksToReturn = cleaningTasks.Select(cleaningTask => new CleaningTaskDTO
        {
            CleaningTaskId = cleaningTask.Id,
            TaskName = cleaningTask.TaskName,
            IsActive = cleaningTask.IsActive,
            OrganizationId = cleaningTask.OrganizationId
        }).ToList();

        return cleaningTasksToReturn;
    }

    public async Task<CleaningTaskDTO> CreateCleaningTaskAsync(CleaningTaskDTO cleaningTaskDTO)
    {
        if (cleaningTaskDTO == null)
        {
            throw new ArgumentNullException(nameof(cleaningTaskDTO));
        }

        var cleaningTask = new CleaningTask
        {
            TaskName = cleaningTaskDTO.TaskName,
            IsActive = cleaningTaskDTO.IsActive,
            OrganizationId = cleaningTaskDTO.OrganizationId,
            CreatedByUserId = cleaningTaskDTO.UserId,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        _unitOfWork.Repository<CleaningTask>().Add(cleaningTask);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null!;
        }

        // Map the created CleaningTask back to CleaningTaskDTO
        var createdCleaningTaskDTO = new CleaningTaskDTO
        {
            CleaningTaskId = cleaningTask.Id,
            OrganizationId = cleaningTask.OrganizationId,
            UserId = cleaningTask.CreatedByUserId,
            TaskName = cleaningTask.TaskName,
            IsActive = cleaningTask.IsActive,
        };

        return createdCleaningTaskDTO;
    }

    public async Task<bool> EditCleaningTaskAsync(CleaningTaskDTO cleaningTaskDTO)
    {
        if (cleaningTaskDTO == null)
        {
            throw new ArgumentNullException(nameof(cleaningTaskDTO));
        }

        var existingCleaningTask = await _unitOfWork.Repository<CleaningTask>().GetByIdAsync(cleaningTaskDTO.CleaningTaskId);

        if (existingCleaningTask == null)
        {
            return false;
        }

        existingCleaningTask.TaskName = cleaningTaskDTO.TaskName;
        existingCleaningTask.IsActive = cleaningTaskDTO.IsActive;

        if (cleaningTaskDTO.IsActive)
        {
            existingCleaningTask.EditedByUserId = cleaningTaskDTO.UserId;
            existingCleaningTask.EditedDate = DateTime.Now;
        }
        else
        {
            existingCleaningTask.IsDeleted = true;
            existingCleaningTask.DeletedByUserId = cleaningTaskDTO.UserId;
            existingCleaningTask.DeletedDate = DateTime.Now;
        }

        _unitOfWork.Repository<CleaningTask>().Update(existingCleaningTask);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteCleaningTaskAsync(int cleaningTaskId)
    {
        var existingCleaningTask = await _unitOfWork.Repository<CleaningTask>().GetByIdAsync(cleaningTaskId) ?? throw new ArgumentException("CleaningTask not found");
        _unitOfWork.Repository<CleaningTask>().Delete(existingCleaningTask);

        var result = await _unitOfWork.Complete();

        return result > 0;
    }



    public async Task<ResponseStatus> CreateCleaningLogAsync(CreateAccommodationCleaningDTO createAccommodationCleaningDTO)
    {
        var existingAccommodation = await _unitOfWork.Repository<Accommodation>().GetByIdAsync(createAccommodationCleaningDTO.AccommodationId);

        if (existingAccommodation == null)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Error,
                Message = "Accommodation not found",
            };
        }

        existingAccommodation.AccommodationStatusId = createAccommodationCleaningDTO.NewAccommodationStatusId;

        var accommodationStatusHistory = new AccommodationStatusHistory
        {
            AccommodationId = createAccommodationCleaningDTO.AccommodationId,
            AccommodationStatusId = createAccommodationCleaningDTO.NewAccommodationStatusId,
            StatusChangeDate = DateTime.Now,
            OrganizationId = createAccommodationCleaningDTO.OrganizationId,
            CreatedByUserId = createAccommodationCleaningDTO.UserId,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        _unitOfWork.Repository<AccommodationStatusHistory>().Add(accommodationStatusHistory);

        var cleaningLog = new CleaningLog
        {
            AccommodationId = createAccommodationCleaningDTO.AccommodationId,
            UserId = createAccommodationCleaningDTO.UserId,
            LogDate = DateTime.Now,
            Comment = createAccommodationCleaningDTO.Comment,
            NeedsMaintenance = createAccommodationCleaningDTO.NeedsMaintenance,
            OrganizationId = createAccommodationCleaningDTO.OrganizationId,
            CreatedByUserId = createAccommodationCleaningDTO.UserId,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        _unitOfWork.Repository<CleaningLog>().Add(cleaningLog);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Failed,
                Message = "Create cleaning log failed",
            };
        }

        int cleaningLogId = cleaningLog.Id;

        var cleaningLogDetails = new List<CleaningLogDetail>();

        if (createAccommodationCleaningDTO.CleaningTaskIds != null && createAccommodationCleaningDTO.CleaningTaskIds.Count > 0)
        {
            foreach (int cleaningTaskId in createAccommodationCleaningDTO.CleaningTaskIds)
            {
                var cleaningLogDetail = new CleaningLogDetail
                {
                    CleaningLogId = cleaningLogId,
                    CleaningTaskId = cleaningTaskId,
                    OrganizationId = createAccommodationCleaningDTO.OrganizationId,
                    CreatedByUserId = createAccommodationCleaningDTO.UserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                cleaningLogDetails.Add(cleaningLogDetail);
            }
        }

        _unitOfWork.Repository<CleaningLogDetail>().AddRange(cleaningLogDetails);

        result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Failed,
                Message = "Create cleaning log failed",
            };
        }

        return new ResponseStatus
        {
            Status = ResponseEnum.Success,
            Message = "Log cleaned successfully",
        };
    }
}
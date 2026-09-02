using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Common;
using ClubPOS.Infrastructure.IRepositories;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;

namespace ClubPOS.Services.Services;

public class MaintenanceService : IMaintenanceService
{
    private readonly IUnitOfWork _unitOfWork;
    public MaintenanceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<MaintenanceTaskDTO>> GetMaintenanceTasksAsync(int organizationId)
    {
        // var maintenanceTasks = await _unitOfWork.Repository<MaintenanceTask>().ListAllAsync();
        var maintenanceTasks = await _unitOfWork.Repository<MaintenanceTask>().ListAllWhereAsync(task => task.OrganizationId == organizationId);

        var maintenanceTasksToReturn = maintenanceTasks.Select(maintenanceTask => new MaintenanceTaskDTO
        {
            MaintenanceTaskId = maintenanceTask.Id,
            TaskName = maintenanceTask.TaskName,
            IsEndUserFixable = maintenanceTask.IsEndUserFixable,
            IsActive = maintenanceTask.IsActive
        }).ToList();

        return maintenanceTasksToReturn;
    }

    public async Task<MaintenanceTaskDTO> CreateMaintenanceTaskAsync(MaintenanceTaskDTO maintenanceTaskDTO)
    {
        if (maintenanceTaskDTO == null)
        {
            throw new ArgumentNullException(nameof(maintenanceTaskDTO));
        }

        var maintenanceTask = new MaintenanceTask
        {
            TaskName = maintenanceTaskDTO.TaskName,
            IsEndUserFixable = maintenanceTaskDTO.IsEndUserFixable,
            IsActive = maintenanceTaskDTO.IsActive,
            OrganizationId = maintenanceTaskDTO.OrganizationId,
            CreatedByUserId = maintenanceTaskDTO.UserId,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        _unitOfWork.Repository<MaintenanceTask>().Add(maintenanceTask);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null!;
        }

        // Map the created MaintenanceTask back to MaintenanceTaskDTO
        var createdMaintenanceTaskDTO = new MaintenanceTaskDTO
        {
            MaintenanceTaskId = maintenanceTask.Id,
            TaskName = maintenanceTask.TaskName,
            IsEndUserFixable = maintenanceTask.IsEndUserFixable,
            IsActive = maintenanceTask.IsActive,
            OrganizationId = maintenanceTask.OrganizationId
        };

        return createdMaintenanceTaskDTO;
    }

    public async Task<bool> EditMaintenanceTaskAsync(MaintenanceTaskDTO maintenanceTaskDTO)
    {
        if (maintenanceTaskDTO == null)
        {
            throw new ArgumentNullException(nameof(maintenanceTaskDTO));
        }

        var existingMaintenanceTask = await _unitOfWork.Repository<MaintenanceTask>().GetByIdAsync(maintenanceTaskDTO.MaintenanceTaskId);

        if (existingMaintenanceTask == null)
        {
            return false;
        }

        existingMaintenanceTask.TaskName = maintenanceTaskDTO.TaskName;
        existingMaintenanceTask.IsEndUserFixable = maintenanceTaskDTO.IsEndUserFixable;
        existingMaintenanceTask.IsActive = maintenanceTaskDTO.IsActive;

        if (maintenanceTaskDTO.IsActive)
        {
            existingMaintenanceTask.EditedByUserId = maintenanceTaskDTO.UserId;
            existingMaintenanceTask.EditedDate = DateTime.Now;
        }
        else
        {
            existingMaintenanceTask.IsDeleted = true;
            existingMaintenanceTask.DeletedByUserId = maintenanceTaskDTO.UserId;
            existingMaintenanceTask.DeletedDate = DateTime.Now;
        }

        _unitOfWork.Repository<MaintenanceTask>().Update(existingMaintenanceTask);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return false!;
        }

        return true;
    }

    public async Task<bool> DeleteMaintenanceTaskAsync(int maintenanceTaskId)
    {
        var existingMaintenanceTask = await _unitOfWork.Repository<MaintenanceTask>().GetByIdAsync(maintenanceTaskId) ?? throw new ArgumentException("MaintenanceTask not found");
        _unitOfWork.Repository<MaintenanceTask>().Delete(existingMaintenanceTask);
        var result = await _unitOfWork.Complete();

        return result > 0;
    }



    public async Task<IReadOnlyList<MaintenanceLogDTO>> GetMaintenanceLogsAsync(int organizationId)
    {
        var maintenanceLogs = await _unitOfWork.Repository<MaintenanceLog>().ListAllWhereAsync(x => x.OrganizationId == organizationId);
        var maintenanceTasks = await _unitOfWork.Repository<MaintenanceTask>().ListAllWhereAsync(x => x.OrganizationId == organizationId);
        var maintenanceLogDetails = await _unitOfWork.Repository<MaintenanceLogDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId);

        var maintenanceLogsToReturn = maintenanceLogs.Select(maintenanceLog => new MaintenanceLogDTO
        {
            MaintenanceLogId = maintenanceLog.Id,
            AccommodationId = maintenanceLog.AccommodationId,
            UserId = maintenanceLog.UserId,
            LogDate = maintenanceLog.LogDate,
            BackInServiceDate = maintenanceLog.BackInServiceDate,
            Comment = maintenanceLog.Comment,
            ResolvedComment = maintenanceLog.ResolvedComment,
            NeedsCleaning = maintenanceLog.NeedsCleaning,
            ResolvedUserId = maintenanceLog.ResolvedUserId,
            OrganizationId = maintenanceLog.OrganizationId,
            MaintenanceLogDetails = maintenanceLogDetails.Where(maintenanceLogDetail => maintenanceLogDetail.MaintenanceLogId == maintenanceLog.Id).Select(maintenanceLogDetail => new MaintenanceLogDetailDTO
            {
                MaintenanceLogDetailId = maintenanceLogDetail.Id,
                MaintenanceTaskId = maintenanceLogDetail.MaintenanceTaskId,
                TaskName = maintenanceTasks.FirstOrDefault(mt => mt.Id == maintenanceLogDetail.MaintenanceTaskId)?.TaskName,
                IsResolved = maintenanceLogDetail.IsResolved,
                DateResolved = maintenanceLogDetail.DateResolved
            }).ToList()
        }).ToList();

        return maintenanceLogsToReturn;
    }

    public async Task<IReadOnlyList<MaintenanceLogDTO>> GetMaintenanceLogsForSelectedAccommodationAsync(int accommodationId, int organizationId)
    {
        var maintenanceLogs = await _unitOfWork.Repository<MaintenanceLog>().ListAllWhereAsync(x => x.OrganizationId == organizationId);
        var maintenanceTasks = await _unitOfWork.Repository<MaintenanceTask>().ListAllWhereAsync(x => x.OrganizationId == organizationId);
        var maintenanceLogDetails = await _unitOfWork.Repository<MaintenanceLogDetail>().ListAllWhereAsync(x => x.OrganizationId == organizationId);

        var maintenanceLogsToReturn = maintenanceLogs
            .Where(maintenanceLog => maintenanceLog.AccommodationId == accommodationId)
            .Select(maintenanceLog => new MaintenanceLogDTO
            {
                MaintenanceLogId = maintenanceLog.Id,
                AccommodationId = maintenanceLog.AccommodationId,
                UserId = maintenanceLog.UserId,
                LogDate = maintenanceLog.LogDate,
                BackInServiceDate = maintenanceLog.BackInServiceDate,
                Comment = maintenanceLog.Comment,
                ResolvedComment = maintenanceLog.ResolvedComment,
                NeedsCleaning = maintenanceLog.NeedsCleaning,
                ResolvedUserId = maintenanceLog.ResolvedUserId,
                OrganizationId = maintenanceLog.OrganizationId,
                MaintenanceLogDetails = maintenanceLogDetails.Where(maintenanceLogDetail => maintenanceLogDetail.MaintenanceLogId == maintenanceLog.Id).Select(maintenanceLogDetail => new MaintenanceLogDetailDTO
                {
                    MaintenanceLogDetailId = maintenanceLogDetail.Id,
                    MaintenanceTaskId = maintenanceLogDetail.MaintenanceTaskId,
                    TaskName = maintenanceTasks.FirstOrDefault(mt => mt.Id == maintenanceLogDetail.MaintenanceTaskId)?.TaskName,
                    IsResolved = maintenanceLogDetail.IsResolved,
                    DateResolved = maintenanceLogDetail.DateResolved
                }).ToList()
            }).ToList();

        return maintenanceLogsToReturn;
    }

    public async Task<ChangeResult> CreateMaintenanceLogAsync(CreateAccommodationMaintenanceDTO createAccommodationMaintenanceDTO)
    {
        var existingAccommodation = await _unitOfWork.Repository<Accommodation>().GetByIdAsync(createAccommodationMaintenanceDTO.AccommodationId);

        if (existingAccommodation == null)
        {
            return ChangeResult.NotFound;
        }

        existingAccommodation.AccommodationStatusId = createAccommodationMaintenanceDTO.NewAccommodationStatusId;

        var accommodationStatusHistory = new AccommodationStatusHistory
        {
            AccommodationId = createAccommodationMaintenanceDTO.AccommodationId,
            AccommodationStatusId = createAccommodationMaintenanceDTO.NewAccommodationStatusId,
            StatusChangeDate = DateTime.Now,
            OrganizationId = createAccommodationMaintenanceDTO.OrganizationId,
            CreatedByUserId = createAccommodationMaintenanceDTO.UserId,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        _unitOfWork.Repository<AccommodationStatusHistory>().Add(accommodationStatusHistory);

        var maintenanceLog = new MaintenanceLog
        {
            AccommodationId = createAccommodationMaintenanceDTO.AccommodationId,
            UserId = createAccommodationMaintenanceDTO.UserId,
            LogDate = DateTime.Now,
            Comment = createAccommodationMaintenanceDTO.Comment,
            NeedsCleaning = false,
            OrganizationId = createAccommodationMaintenanceDTO.OrganizationId,
            CreatedByUserId = createAccommodationMaintenanceDTO.UserId,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        _unitOfWork.Repository<MaintenanceLog>().Add(maintenanceLog);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return ChangeResult.Failure;
        }

        int maintenanceLogId = maintenanceLog.Id;

        var maintenanceLogDetails = new List<MaintenanceLogDetail>();

        if (createAccommodationMaintenanceDTO.MaintenanceTaskIds != null && createAccommodationMaintenanceDTO.MaintenanceTaskIds.Count > 0)
        {
            foreach (int maintenanceTaskId in createAccommodationMaintenanceDTO.MaintenanceTaskIds)
            {
                var maintenanceLogDetail = new MaintenanceLogDetail
                {
                    MaintenanceLogId = maintenanceLogId,
                    MaintenanceTaskId = maintenanceTaskId,
                    IsResolved = false,
                    DateResolved = null,
                    OrganizationId = createAccommodationMaintenanceDTO.OrganizationId,
                    CreatedByUserId = createAccommodationMaintenanceDTO.UserId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                maintenanceLogDetails.Add(maintenanceLogDetail);
            }
        }

        _unitOfWork.Repository<MaintenanceLogDetail>().AddRange(maintenanceLogDetails);

        result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return ChangeResult.Failure;
        }

        return ChangeResult.Success;
    }

    public async Task<ResponseStatus> EditMaintenanceLogAsync(EditAccommodationMaintenanceDTO editAccommodationMaintenanceDTO)
    {
        if (editAccommodationMaintenanceDTO == null)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Error,
                Message = "Invalid payload",
            };
        }

        var existingAccommodation = await _unitOfWork.Repository<Accommodation>().GetByIdAsync(editAccommodationMaintenanceDTO.AccommodationId);
        if (existingAccommodation == null)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Error,
                Message = "Accommodation not found",
            };
        }

        var maintenanceLogs = await _unitOfWork.Repository<MaintenanceLog>().ListAllWhereAsync(x => x.OrganizationId == editAccommodationMaintenanceDTO.OrganizationId);
        var maintenanceLogDetails = await _unitOfWork.Repository<MaintenanceLogDetail>().ListAllWhereAsync(x => x.OrganizationId == editAccommodationMaintenanceDTO.OrganizationId);
        var lastMaintenanceLog = maintenanceLogs
                    .Where(ml => ml.AccommodationId == editAccommodationMaintenanceDTO.AccommodationId)
                    .OrderByDescending(ml => ml.LogDate)
                    .FirstOrDefault();
        if (lastMaintenanceLog == null)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Error,
                Message = "Last maintenance log not found",
            };
        }
        var requiredMaintenanceLogDetails = maintenanceLogDetails.Where(mld => mld.MaintenanceLogId == lastMaintenanceLog.Id && !mld.IsResolved).ToList();
        var requiredMaintenanceTaskIds = requiredMaintenanceLogDetails.Select(mld => mld.MaintenanceTaskId).ToList();

        if (editAccommodationMaintenanceDTO.MaintenanceTaskIds == null)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Error,
                Message = "No maintenance task selected",
            };
        }
        if (editAccommodationMaintenanceDTO.MaintenanceTaskIds.Count < requiredMaintenanceTaskIds.Count)
        {
            foreach (var taskId in editAccommodationMaintenanceDTO.MaintenanceTaskIds)
            {
                if (requiredMaintenanceTaskIds.Contains(taskId))
                {
                    var maintenanceLogDetailToUpdate = requiredMaintenanceLogDetails.Where(mld => mld.MaintenanceTaskId == taskId).FirstOrDefault();
                    if (maintenanceLogDetailToUpdate != null)
                    {
                        maintenanceLogDetailToUpdate.IsResolved = true;
                        maintenanceLogDetailToUpdate.DateResolved = DateTime.Now;
                        maintenanceLogDetailToUpdate.EditedByUserId = editAccommodationMaintenanceDTO.UserId;
                        maintenanceLogDetailToUpdate.EditedDate = DateTime.Now;
                        maintenanceLogDetailToUpdate.IsDeleted = false;
                        _unitOfWork.Repository<MaintenanceLogDetail>().Update(maintenanceLogDetailToUpdate);
                    }
                    else
                    {
                        return new ResponseStatus
                        {
                            Status = ResponseEnum.Error,
                            Message = "No maintenance task found",
                        };
                    }
                }
            }
        }
        else
        {
            lastMaintenanceLog.UserId = editAccommodationMaintenanceDTO.UserId;
            lastMaintenanceLog.BackInServiceDate = DateTime.Now;
            lastMaintenanceLog.ResolvedComment = editAccommodationMaintenanceDTO.ResolvedComment;
            lastMaintenanceLog.ResolvedUserId = editAccommodationMaintenanceDTO.ResolvedUserId;
            lastMaintenanceLog.NeedsCleaning = editAccommodationMaintenanceDTO.NeedsCleaning;
            lastMaintenanceLog.EditedByUserId = editAccommodationMaintenanceDTO.UserId;
            lastMaintenanceLog.EditedDate = DateTime.Now;
            lastMaintenanceLog.IsDeleted = false;

            foreach (var taskId in editAccommodationMaintenanceDTO.MaintenanceTaskIds)
            {
                if (requiredMaintenanceTaskIds.Contains(taskId))
                {
                    var maintenanceLogDetailToUpdate = requiredMaintenanceLogDetails.Where(mld => mld.MaintenanceTaskId == taskId).FirstOrDefault();
                    if (maintenanceLogDetailToUpdate != null)
                    {
                        maintenanceLogDetailToUpdate.IsResolved = true;
                        maintenanceLogDetailToUpdate.DateResolved = DateTime.Now;
                        maintenanceLogDetailToUpdate.EditedByUserId = editAccommodationMaintenanceDTO.UserId;
                        maintenanceLogDetailToUpdate.EditedDate = DateTime.Now;
                        maintenanceLogDetailToUpdate.IsDeleted = false;
                        _unitOfWork.Repository<MaintenanceLogDetail>().Update(maintenanceLogDetailToUpdate);
                    }
                    else
                    {
                        return new ResponseStatus
                        {
                            Status = ResponseEnum.Error,
                            Message = "No maintenance log found",
                        };
                    }
                }
            }
            existingAccommodation.AccommodationStatusId = editAccommodationMaintenanceDTO.NewAccommodationStatusId;

            _unitOfWork.Repository<MaintenanceLog>().Update(lastMaintenanceLog);
        }

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return new ResponseStatus
            {
                Status = ResponseEnum.Failed,
                Message = "Update maintenance log failed",
            };
        }

        return new ResponseStatus
        {
            Status = ResponseEnum.Success,
            Message = "Maintenance log updated successfully",
        };
    }
}
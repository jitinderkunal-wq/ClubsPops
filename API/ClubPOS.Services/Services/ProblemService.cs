using ClubPOS.Core.Entities;
using ClubPOS.Infrastructure.IRepositories;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;

namespace ClubPOS.Services.Services;

public class ProblemService : IProblemService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProblemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProblemDTO?> GetProblemByIdAsync(int problemId)
    {
        var problem = await _unitOfWork.Repository<Problem>().GetByIdAsync(problemId);

        if (problem == null)
        {
            return null;
        };

        var problemDTO = new ProblemDTO
        {
            ProblemId = problem.Id,
            BookingId = problem.BookingId,
            ProblemCategoryId = problem.ProblemCategoryId,
            ProblemExplaination = problem.ProblemExplaination,
            UserId = problem.UserId,
            DateCreated = problem.DateCreated,
            NewMemberName = problem.NewMemberName
        };

        return problemDTO;
    }

    public async Task<IReadOnlyList<ProblemDTO>> GetProblemsAsync()
    {
        var problems = await _unitOfWork.Repository<Problem>().ListAllAsync();

        var problemDTOs = problems.Select(problem => new ProblemDTO
        {
            ProblemId = problem.Id,
            BookingId = problem.BookingId,
            ProblemCategoryId = problem.ProblemCategoryId,
            ProblemExplaination = problem.ProblemExplaination,
            UserId = problem.UserId,
            DateCreated = problem.DateCreated,
            NewMemberName = problem.NewMemberName
        }).ToList();

        return problemDTOs;
    }

    public async Task<ProblemDTO> CreateProblemAsync(ProblemCreateDTO problemCreateDTO)
    {
        if (problemCreateDTO == null)
        {
            throw new ArgumentNullException(nameof(problemCreateDTO));
        }

        var problem = new Problem
        {
            BookingId = problemCreateDTO.BookingId,
            ProblemCategoryId = problemCreateDTO.ProblemCategoryId,
            ProblemExplaination = problemCreateDTO.ProblemExplaination,
            UserId = problemCreateDTO.UserId,
            DateCreated = DateTime.Now,
            NewMemberName = problemCreateDTO.NewMemberName
        };

        _unitOfWork.Repository<Problem>().Add(problem);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null!;
        }

        var problemDTO = new ProblemDTO
        {
            ProblemId = problem.Id,
            BookingId = problem.BookingId,
            ProblemCategoryId = problem.ProblemCategoryId,
            ProblemExplaination = problem.ProblemExplaination,
            UserId = problem.UserId,
            DateCreated = problem.DateCreated,
            NewMemberName = problem.NewMemberName
        };

        return problemDTO;
    }

    public async Task<bool> DeleteProblemAsync(int problemId)
    {
        var existingProblem = await _unitOfWork.Repository<Problem>().GetByIdAsync(problemId);

        if (existingProblem == null)
        {
            throw new ArgumentException("Problem not found");
        }

        _unitOfWork.Repository<Problem>().Delete(existingProblem);
        var result = await _unitOfWork.Complete();

        return result > 0;
    }
}
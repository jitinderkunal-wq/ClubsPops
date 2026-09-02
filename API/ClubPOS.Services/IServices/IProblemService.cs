using ClubPOS.Services.DTOs;

namespace ClubPOS.Services.IServices;

public interface IProblemService
{
    Task<ProblemDTO?> GetProblemByIdAsync(int problemId);
    Task<IReadOnlyList<ProblemDTO>> GetProblemsAsync();
    Task<ProblemDTO> CreateProblemAsync(ProblemCreateDTO problemCreateDTO);
    Task<bool> DeleteProblemAsync(int problemId);
}
using ClubPOS.API.Errors;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ClubPOS.API.Controllers;

public class ProblemController : BaseApiController
{
    private readonly IProblemService _problemService;
    public ProblemController(IProblemService problemService)
    {
        _problemService = problemService;
    }

    [HttpGet("{problemId}")]
    public async Task<ActionResult<ProblemDTO>> GetProblemByIdAsync(int problemId)
    {
        var problem = await _problemService.GetProblemByIdAsync(problemId);

        if (problem == null)
        {
            return NotFound(new ApiResponse(404));
        }

        return Ok(problem);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProblemDTO>>> GetProblemsAsync()
    {
        var organizations = await _problemService.GetProblemsAsync();

        return Ok(organizations);
    }

    [HttpPost]
    public async Task<ActionResult<ProblemDTO>> CreateProblemAsync(ProblemCreateDTO problemCreateDTO)
    {
        var newProblem = await _problemService.CreateProblemAsync(problemCreateDTO);

        if (newProblem == null)
        {
            return BadRequest("Issue in creating new problem");
        }

        return Ok(newProblem);
    }

    [HttpDelete("{problemId}")]
    public async Task<ActionResult<bool>> DeleteProblemAsync(int problemId)
    {
        var isProblemDeleted = await _problemService.DeleteProblemAsync(problemId);
        return Ok(isProblemDeleted);
    }
}
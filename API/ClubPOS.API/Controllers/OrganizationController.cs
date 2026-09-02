using ClubPOS.API.Errors;
using ClubPOS.Core.Entities;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ClubPOS.API.Controllers;

[Authorize]
public class OrganizationController : BaseApiController
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet("{organizationId}")]
    public async Task<ActionResult<Organization>> GetOrganizationByIdAsync(int organizationId)
    {
        var organization = await _organizationService.GetOrganizationByIdAsync(organizationId);
        if (organization == null) return NotFound(new ApiResponse(404));
        return Ok(organization);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Organization>>> GetOrganizationsAsync()
    {
        var organizations = await _organizationService.GetOrganizationsAsync();
        return Ok(organizations);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost("uploadLogo")]
    public async Task<ActionResult<bool>> UploadLogoAsync([FromForm] IFormFile file)
    {
        var newOrganization = await _organizationService.UploadLogoAsync(file);
        if (!newOrganization) return BadRequest("Problem uploading Organization logo");
        return Ok(true);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost]
    public async Task<ActionResult<Organization>> CreateOrganizationAsync(OrganizationCreateDTO organizationCreateDTO)
    {
        var newOrganization = await _organizationService.CreateOrganizationAsync(organizationCreateDTO);
        if (newOrganization == null) return BadRequest("Problem Creating Organization");
        return Ok(newOrganization);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPatch]
    public async Task<ActionResult<bool>> EditOrganizationAsync(Organization organization)
    {
        if (organization == null) return BadRequest("Invalid organization data");
        var isOrganizationUpdated = await _organizationService.EditOrganizationAsync(organization);
        if (!isOrganizationUpdated) return NotFound("Organization you wanted to update was not found");
        return Ok(isOrganizationUpdated);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpDelete("{organizationId}")]
    public async Task<ActionResult<bool>> DeleteOrganizationAsync(int OrganizationId)
    {
        var isOrganizationDeleted = await _organizationService.DeleteOrganizationAsync(OrganizationId);
        return Ok(isOrganizationDeleted);
    }
}
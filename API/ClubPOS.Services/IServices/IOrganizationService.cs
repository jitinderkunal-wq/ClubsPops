using ClubPOS.Core.Entities;
using ClubPOS.Services.DTOs;
using Microsoft.AspNetCore.Http;

namespace ClubPOS.Services.IServices;

public interface IOrganizationService
{
    Task<Organization?> GetOrganizationByIdAsync(int organizationId);
    Task<IReadOnlyList<Organization>> GetOrganizationsAsync();
    Task<bool> UploadLogoAsync(IFormFile file);
    Task<Organization> CreateOrganizationAsync(OrganizationCreateDTO organizationCreateDTO);
    Task<bool> EditOrganizationAsync(Organization organization);
    Task<bool> DeleteOrganizationAsync(int organizationId);
}
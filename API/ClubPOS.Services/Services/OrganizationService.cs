using ClubPOS.Core.Entities;
using ClubPOS.Infrastructure.IRepositories;
using ClubPOS.Services.DTOs;
using ClubPOS.Services.IServices;
using Microsoft.AspNetCore.Http;

namespace ClubPOS.Services.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;
    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Organization?> GetOrganizationByIdAsync(int organizationId)
    {
        var organization = await _organizationRepository.GetOrganizationByIdAsync(organizationId);
        if (organization == null) return null;
        var organizationToReturn = new Organization
        {
            OrganizationId = organization.OrganizationId,
            OrganizationName = organization.OrganizationName,
            Street = organization.Street,
            City = organization.City,
            Province = organization.Province,
            PostalCode = organization.PostalCode,
            Country = organization.Country,
            RegistrationNumber = organization.RegistrationNumber,
            Logo = organization.Logo,
            IsActive = organization.IsActive,
            CreatedDate = organization.CreatedDate,
            EditedDate = organization.EditedDate,
            DeletedDate = organization.DeletedDate
        };
        return organizationToReturn;
    }

    public async Task<IReadOnlyList<Organization>> GetOrganizationsAsync()
    {
        var organizations = await _organizationRepository.GetOrganizationsAsync();
        var organizationsToReturn = organizations.Select(organization => new Organization
        {
            OrganizationId = organization.OrganizationId,
            OrganizationName = organization.OrganizationName,
            Street = organization.Street,
            City = organization.City,
            Province = organization.Province,
            PostalCode = organization.PostalCode,
            Country = organization.Country,
            RegistrationNumber = organization.RegistrationNumber,
            Logo = organization.Logo,
            IsActive = organization.IsActive,
            CreatedDate = organization.CreatedDate,
            EditedDate = organization.EditedDate,
            DeletedDate = organization.DeletedDate
        }).ToList();
        return organizationsToReturn;
    }

    public async Task<bool> UploadLogoAsync(IFormFile file)
    {
        // File Uploading Start
        try
        {
            if (file == null || file.Length == 0) throw new Exception("Invalid file.");
            //var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Logo");               // Use for full path
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Logo"); 
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<Organization> CreateOrganizationAsync(OrganizationCreateDTO organizationCreateDTO)
    {
        var filePath = "";
        if (organizationCreateDTO.Logo != null)
        {
            var uploads = Path.Combine("Logo");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var combinedPath = Path.Combine(uploads, organizationCreateDTO.Logo);
            filePath = combinedPath.Replace('\\', '/');
        } 
        var organization = new Organization
        {
            OrganizationName = organizationCreateDTO.OrganizationName,
            Street = organizationCreateDTO.Street,
            City = organizationCreateDTO.City,
            Province = organizationCreateDTO.Province,
            PostalCode = organizationCreateDTO.PostalCode,
            Country = organizationCreateDTO.Country,
            RegistrationNumber = organizationCreateDTO.RegistrationNumber,
            Logo = filePath,
            CreatedDate = DateTime.Now,
            IsActive = true,
        };
        var newOrganization = await _organizationRepository.CreateOrganizationAsync(organization);
        // Handle the case when organization creation fails 
        if (newOrganization == null) throw new Exception("Failed to create organization.");
        var organizationToReturn = new Organization
        {
            OrganizationId = newOrganization.OrganizationId,
            OrganizationName = newOrganization.OrganizationName,
            Street = newOrganization.Street,
            City = newOrganization.City,
            Province = newOrganization.Province,
            PostalCode = newOrganization.PostalCode,
            Country = newOrganization.Country,
            RegistrationNumber = newOrganization.RegistrationNumber,
            Logo = newOrganization.Logo,
            IsActive = newOrganization.IsActive,
            CreatedDate = newOrganization.CreatedDate,
            DeletedDate = newOrganization.DeletedDate,
            EditedDate  = newOrganization.EditedDate
        };
        return organizationToReturn;
    }

    public async Task<bool> EditOrganizationAsync(Organization organization)
    {
        var existingOrganization = await _organizationRepository.GetOrganizationByIdAsync(organization.OrganizationId);
        if (existingOrganization == null) throw new ArgumentException("Organization not found");
        if (organization.Logo != existingOrganization.Logo && organization.Logo != null)
        {
            var uploads = Path.Combine("Logo");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var combinedPath = Path.Combine(uploads, organization.Logo);
            var filePath = combinedPath.Replace('\\', '/');
            existingOrganization.Logo = filePath;
        }
        else existingOrganization.Logo = organization.Logo;
        existingOrganization.OrganizationName = organization.OrganizationName;
        existingOrganization.Street = organization.Street;
        existingOrganization.City = organization.City;
        existingOrganization.Province = organization.Province;
        existingOrganization.PostalCode = organization.PostalCode;
        existingOrganization.Country = organization.Country;
        existingOrganization.RegistrationNumber = organization.RegistrationNumber;
        existingOrganization.EditedDate = DateTime.Now;
        existingOrganization.IsActive = organization.IsActive;
        var isOrganizationUpdated = await _organizationRepository.EditOrganizationAsync(existingOrganization);
        return isOrganizationUpdated;
    }

    public async Task<bool> DeleteOrganizationAsync(int organizationId)
    {
        var existingOrganization = await _organizationRepository.GetOrganizationByIdAsync(organizationId);
        if (existingOrganization == null) throw new ArgumentException("Organization not found");
        var isOrganizationDeleted = await _organizationRepository.DeleteOrganizationAsync(organizationId);
        return isOrganizationDeleted;
    }
}
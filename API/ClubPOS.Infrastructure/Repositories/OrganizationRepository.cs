using ClubPOS.Core.Entities;
using ClubPOS.Infrastructure.Data;
using ClubPOS.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClubPOS.Infrastructure.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly DataContext _context;
    private readonly ILogger<OrganizationRepository> _logger;
    public OrganizationRepository(DataContext context, ILogger<OrganizationRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Organization?> GetOrganizationByIdAsync(int organizationId)
    {
        return await _context.Organizations!.FindAsync(organizationId);
    }

    public async Task<IReadOnlyList<Organization>> GetOrganizationsAsync()
    {
        return await _context.Organizations!.ToListAsync();
    }

    public async Task<Organization> CreateOrganizationAsync(Organization organization)
    {
        try
        {
            _context.Organizations!.Add(organization);
            await _context.SaveChangesAsync();
            return organization;
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            _logger.LogError("An error occured while creating Organization : {error}", error);
            return null!;
        }
    }

    public async Task<bool> EditOrganizationAsync(Organization organization)
    {
        try
        {
            _context.Organizations!.Update(organization);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            _logger.LogError("An error occured while editing Organization : {error}", error);
            return false;
        }
    }

    public async Task<bool> DeleteOrganizationAsync(int organizationId)
    {
        try
        {
            var organization = await _context.Organizations!.FindAsync(organizationId);
            if (organization == null)
            {
                return false;
            }
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            _logger.LogError("An error occured while deleting Organization : {error}", error);
            return false;
        }
    }
}
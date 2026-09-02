using ClubPOS.Core.Entities;

namespace ClubPOS.Infrastructure.IRepositories;

public interface IOrganizationRepository
{
    Task<Organization?> GetOrganizationByIdAsync(int organizationId);
    Task<IReadOnlyList<Organization>> GetOrganizationsAsync();
    Task<Organization> CreateOrganizationAsync(Organization organization);
    Task<bool> EditOrganizationAsync(Organization organization);
    Task<bool> DeleteOrganizationAsync(int OrganizationId);
}
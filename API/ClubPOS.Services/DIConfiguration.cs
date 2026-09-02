using ClubPOS.Infrastructure.IRepositories;
using ClubPOS.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClubPOS.Services;

public static class DIConfiguration
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
    }
}
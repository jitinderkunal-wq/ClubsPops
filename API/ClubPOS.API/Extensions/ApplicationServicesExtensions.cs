using ClubPOS.API.Errors;
using ClubPOS.Infrastructure.Data;
using ClubPOS.Services;
using ClubPOS.Services.IServices;
using ClubPOS.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubPOS.API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database connection
        services.AddDbContext<DataContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
        });

        DIConfiguration.RegisterDependencies(services);

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccommodationService, AccommodationService>();
        services.AddScoped<IAdministrativeService, AdministrativeService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<ICleaningService, CleaningService>();
        services.AddScoped<IMaintenanceService, MaintenanceService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IProblemService, ProblemService>();
        services.AddScoped<ITokenService, TokenService>();

        services.Configure<ApiBehaviorOptions>(
            options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value?.Errors!)
                        .Select(x => x.ErrorMessage)
                        .ToArray();
                    var errorResponse = new ApiValidationErrorResponse { Errors = errors };
                    return new BadRequestObjectResult(errorResponse);
                };
            }
        );

        services.AddCors(
            opt =>
            {
                opt.AddPolicy(
                    "CorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    }
                );
            }
        );

        return services;
    }
}
using System.Text;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings;

namespace ClubPOS.API.Extensions;

//[Route("api/[controller]")]
//[ApiController]
public static class IdentityServicesExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
        });

        //services.AddIdentity<IdentityUser, IdentityRole>()
        services.AddIdentityCore<AppUser>(option =>
        {
            // Add identity options here
        })
         .AddRoles<IdentityRole>()
         .AddEntityFrameworkStores<IdentityDbContext>()
         .AddDefaultTokenProviders()
         .AddSignInManager<SignInManager<AppUser>>();

        //Adding Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        // Adding JWT Bearer
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Token:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Token:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]!))
            };
        });
        // End JWT Token Authentication

        #region Old Code

        // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(options =>
        //    {
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]!)),
        //            ValidateIssuer = true,
        //            ValidIssuer = configuration["Token:Issuer"],
        //            ValidateAudience = false
        //        };
        //    });
        // services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy("RequireAdministratorRole", policy =>
        //            policy.RequireRole("manager")); // Example policy requiring "Administrator" role
        //    });

        #endregion

        return services;
    }
}
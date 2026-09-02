using ClubPOS.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ClubPOS.Infrastructure.Identity;

public class IdentityDbContextSeed
{
    public static async Task SeedUsersAndRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Seed Roles
        string[] roles = { "SuperAdmin", "Admin", "FrontCash", "Cleaner" };
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole { Name = roleName };
                await roleManager.CreateAsync(role);
            }
        }

        // Seed Users
        var users = new[]
        {
                new
                {
                    UserName = "super@test.com",
                    FirstName = "Super",
                    LastName = "Admin",
                    PayRate = 0,
                    HireDate = DateTime.Now,
                    SIN = 745892365,
                    IsActive = true,
                    IsSalary = true,
                    InTrainingMode = false,
                    OrganizationId = 1,
                    Email = "super@test.com",
                    Role = "SuperAdmin", // Assign the 'SuperAdmin' role to SuperAdmin
                    UserAddress = new UserAddress
                    {
                        Street = "10th Street",
                        City = "Mohali",
                        Province = "Punjab",
                        PostalCode = "140307",
                        CellPhone = "9328794536"
                    }
                },
                new
                {
                    UserName = "admin@test.com",
                    FirstName = "Admin",
                    LastName = "",
                    PayRate = 150000,
                    HireDate = DateTime.Now,
                    SIN = 745892365,
                    IsActive = true,
                    IsSalary = true,
                    InTrainingMode = false,
                    OrganizationId = 1,
                    Email = "admin@test.com",
                    Role = "Admin", // Assign the 'Admin' role to Admin
                    UserAddress = new UserAddress
                    {
                        Street = "10th Street",
                        City = "Mohali",
                        Province = "Punjab",
                        PostalCode = "140307",
                        CellPhone = "9328794536"
                    }
                },
                new
                {
                    UserName = "front@test.com",
                    FirstName = "Front",
                    LastName = "Doe",
                    PayRate = 150000,
                    HireDate = DateTime.Now,
                    SIN = 745892365,
                    IsActive = true,
                    IsSalary = true,
                    InTrainingMode = false,
                    OrganizationId = 1,
                    Email = "front@test.com",
                    Role = "FrontCash", // Assign the 'FrontCash' role to Front
                    UserAddress = new UserAddress
                    {
                        Street = "11th Street",
                        City = "Mohali",
                        Province = "Punjab",
                        PostalCode = "140308",
                        CellPhone = "9328794537"
                    }
                },
                new
                {
                    UserName = "clean@test.com",
                    FirstName = "Clean",
                    LastName = "Doe",
                    PayRate = 150000,
                    HireDate = DateTime.Now,
                    SIN = 745892365,
                    IsActive = true,
                    IsSalary = true,
                    InTrainingMode = false,
                    OrganizationId = 1,
                    Email = "clean@test.com",
                    Role = "Cleaner", // Assign the 'Cleaner' role to Clean
                    UserAddress = new UserAddress
                    {
                        Street = "12th Street",
                        City = "Mohali",
                        Province = "Punjab",
                        PostalCode = "140309",
                        CellPhone = "9328794538"
                    }
                }
            };

        foreach (var userData in users)
        {
            var existingUser = await userManager.FindByEmailAsync(userData.UserName);
            if (existingUser == null)
            {
                var user = new AppUser
                {
                    UserName = userData.UserName,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    PayRate = userData.PayRate,
                    HireDate = userData.HireDate,
                    SIN = userData.SIN,
                    IsActive = userData.IsActive,
                    IsSalary = userData.IsSalary,
                    InTrainingMode = userData.InTrainingMode,
                    OrganizationId = userData.OrganizationId,
                    Email = userData.Email,
                    UserAddress = userData.UserAddress
                };

                var result = await userManager.CreateAsync(user, "Pa$$w0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userData.Role);
                }
            }
        }
    }
}
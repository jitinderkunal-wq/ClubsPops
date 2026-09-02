using System.Security.Claims;
using ClubPOS.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClubPOS.API.Extensions;

public static class UserManagerExtensions
{
    public static async Task<AppUser?> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);

        var appUser = await userManager.Users.Include(x => x.UserAddress)
            .SingleOrDefaultAsync(x => x.Email == email);

        if (appUser == null)
        {
            return null;
        }

        return appUser;
    }

    public static async Task<AppUser?> FindByEmailFromClaimsPricipal(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);

        var appUser = await userManager.Users.SingleOrDefaultAsync(x => x.Email == email);

        if (appUser == null)
        {
            return null;
        }

        return appUser;
    }
}
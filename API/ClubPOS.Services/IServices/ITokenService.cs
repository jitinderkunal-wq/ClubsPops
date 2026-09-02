using System.Security.Claims;
using ClubPOS.Core.Entities.Identity;

namespace ClubPOS.Services.IServices;

public interface ITokenService
{
    string GenerateJwtToken(IEnumerable<Claim> userSpecificClaims);
}
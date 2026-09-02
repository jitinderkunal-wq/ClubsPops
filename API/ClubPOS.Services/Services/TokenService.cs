using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClubPOS.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClubPOS.Services.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]!));
    }

    public string GenerateJwtToken(IEnumerable<Claim> userSpecificClaims)
    {
        var defaultClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var allClaims = defaultClaims.Concat(userSpecificClaims);
        
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(allClaims),
            Expires = DateTime.Now.AddDays(7),
            Issuer = _configuration["Token:Issuer"],
            Audience = _configuration["Token:Audience"],
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
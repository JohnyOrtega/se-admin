using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(string email)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("WkT1pRfG5vHs8JqKwLcPdXeRgU2yVzMx");
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(email),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = credentials,
        };
        
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(string email)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Email, email));
        return ci;
    }
}
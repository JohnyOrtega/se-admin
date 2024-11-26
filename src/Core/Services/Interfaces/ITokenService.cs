using System.Security.Claims;
using Core.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services.Interfaces;

public interface ITokenService
{
    (string, DateTime) GenerateToken(IUserToken user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
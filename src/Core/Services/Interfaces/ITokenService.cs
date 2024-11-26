using System.Security.Claims;
using Core.Models.Interfaces;

namespace Core.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(IUserToken user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
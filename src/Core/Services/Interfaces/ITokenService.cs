using System.Security.Claims;
using Core.Models;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface ITokenService
{
    AuthResponse GenerateAccessToken(User user);
    ClaimsPrincipal GetPrincipal(string accessToken, string refreshToken);
}
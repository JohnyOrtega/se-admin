using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Models;
using Core.Models.Interfaces;
using Core.Models.Response;
using Core.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

public class TokenService(
    ITokenConfiguration tokenConfiguration) : ITokenService
{
    private readonly ITokenConfiguration _tokenConfiguration = tokenConfiguration;

    private string GenerateAccessTokenFromClaims(List<Claim> claims)
    {
        var secretKey = _tokenConfiguration.SecretKey
                        ?? throw new InvalidOperationException("Secret Key is missing.");

        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_tokenConfiguration.ExpirationInHours),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = _tokenConfiguration.Issuer,
            Audience = _tokenConfiguration.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public AuthResponse GenerateAccessToken(User user)
    {
        var claims = GenerateClaims(user);
        var accessToken = GenerateAccessTokenFromClaims(claims);
        var refreshToken = GenerateRefreshToken();

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public string GenerateAccessToken(ClaimsPrincipal principal)
    {
        var claims = principal.Claims.ToList();
        return GenerateAccessTokenFromClaims(claims);
    }

    private static List<Claim> GenerateClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role),
        };

        return claims;
    }

    public ClaimsPrincipal GetPrincipal(string accessToken, string refreshToken)
    {
        var secretKey = _tokenConfiguration.SecretKey
                        ?? throw new InvalidOperationException("Secret Key is missing.");

        var key = Encoding.UTF8.GetBytes(secretKey);

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _tokenConfiguration.Issuer,
            ValidateAudience = true,
            ValidAudience = _tokenConfiguration.Audience,
            ValidateLifetime = false
        };

        try
        {
            var principal = tokenHandler.ValidateToken(
                accessToken,
                validationParameters,
                out var securityToken
            );

            return principal;
        }
        catch
        {
            throw new UnauthorizedAccessException("Refresh token inválido");
        }
    }


}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Models.Interfaces;
using Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

public class TokenService(ITokenConfiguration tokenConfiguration, IConfiguration configuration)
    : ITokenService
{
    private readonly ITokenConfiguration _tokenConfiguration = tokenConfiguration;
    private readonly IConfiguration _configuration = configuration;
    
    public (string, DateTime) GenerateToken(IUserToken user)
    {
        var secretKey = _configuration["Jwt:SecretKey"] 
                        ?? throw new InvalidOperationException("Secret key não configurada");
        
        var key = Encoding.ASCII.GetBytes(secretKey);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
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
        return (tokenHandler.WriteToken(token), token.ValidTo);
    }

    private static ClaimsIdentity GenerateClaims(IUserToken user)
    {
        var claims = new ClaimsIdentity();
        
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
        claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        claims.AddClaim(new Claim(ClaimTypes.Role, user.Role));

        return claims;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var secretKey = _configuration["Jwt:SecretKey"] 
                        ?? throw new InvalidOperationException("SecretKey cannot be null.");
        
        var key = Encoding.ASCII.GetBytes(secretKey);
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false,
            ValidIssuer = _tokenConfiguration.Issuer,
            ValidAudience = _tokenConfiguration.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        
        try
        {
            var principal = tokenHandler.ValidateToken(
                token, 
                tokenValidationParameters, 
                out var securityToken);
            
            if (securityToken is not JwtSecurityToken jwtToken || 
                !IsJwtWithValidAlgorithm(jwtToken))
            {
                return null;
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }
    
    private static bool IsJwtWithValidAlgorithm(JwtSecurityToken jwtToken)
    {
        Console.WriteLine(jwtToken.Header.Alg);
        
        return jwtToken.Header.Alg.Equals("HS256", StringComparison.InvariantCultureIgnoreCase) ||
               jwtToken.SignatureAlgorithm == SecurityAlgorithms.HmacSha256Signature;
    }
}
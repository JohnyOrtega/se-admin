using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Models;
using Core.Models.Interfaces;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

public class TokenService(
    ITokenConfiguration tokenConfiguration, 
    IConfiguration configuration, 
    IUserRepository userRepository)
    : ITokenService
{
    private readonly ITokenConfiguration _tokenConfiguration = tokenConfiguration;
    private readonly IConfiguration _configuration = configuration;
    private readonly IUserRepository _userRepository = userRepository;

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
    
    public string GenerateAccessToken(User user)
    {
        var claims = GenerateClaims(user);
        var accessToken = GenerateAccessTokenFromClaims(claims);
        return accessToken;
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
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        return claims;
    }
    
    private static bool IsJwtWithValidAlgorithm(JwtSecurityToken jwtToken)
    {
        Console.WriteLine(jwtToken.Header.Alg);
        
        return jwtToken.Header.Alg.Equals("HS256", StringComparison.InvariantCultureIgnoreCase) ||
               jwtToken.SignatureAlgorithm == SecurityAlgorithms.HmacSha256Signature;
    }
}
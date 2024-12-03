using Core.Models.Interfaces;

namespace Core.Models;

public class TokenConfiguration : ITokenConfiguration
{
    public string SecretKey { get; set; } = string.Empty;
    public int ExpirationInHours { get; set; } = 1;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
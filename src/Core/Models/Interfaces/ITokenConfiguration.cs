namespace Core.Models.Interfaces;

public interface ITokenConfiguration
{
    string SecretKey { get; }
    int ExpirationInHours { get; }
    string Issuer { get; }
    string Audience { get; }
}
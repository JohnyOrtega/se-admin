namespace Core.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(string email);
}
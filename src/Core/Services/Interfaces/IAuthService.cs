namespace Core.Services.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterUser(string email, string password);
    Task<string?> LoginUser(string email, string password);
}
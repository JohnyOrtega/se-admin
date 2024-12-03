using Core.Dtos.Login;
using Core.Models;

namespace Core.Services.Interfaces;

public interface IAuthService
{
    Task<User?> ValidateUserCredentials(LoginRequestDto request);
    Task<User> GetAuthUser(Guid id);
}
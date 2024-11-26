using Core.Dtos;
using Core.Dtos.Login;

namespace Core.Services.Interfaces;

public interface IUserService
{
    Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request);
    Task<LoginResponseDto?> RefreshTokenAsync(string token, string refreshToken);
}
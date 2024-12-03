using Core.Dtos.Login;

namespace Core.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request);
    Task<LoginResponseDto?> RefreshTokenAsync(string token, string refreshToken);
}
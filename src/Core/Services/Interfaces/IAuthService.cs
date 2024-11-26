using Core.Dtos;
using Core.Dtos.Login;
using Core.Dtos.Register;

namespace Core.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request);
    Task<LoginResponseDto?> RefreshTokenAsync(string token, string refreshToken);
    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
}
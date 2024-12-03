using System.Security.Claims;
using Core.Dtos.Login;
using Core.Models;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.Services;

public class AuthService(IPasswordHasher<User> passwordHasher, 
    ITokenService tokenService, 
    IUserRepository userRepository) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    
    public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null) 
            return null;
        
        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(
            user, 
            user.PasswordHash, 
            request.Password
        );
        
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return null;
        
        var userToken = new UserToken
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role
        };
        
        var (token, tokenExpiration) = _tokenService.GenerateToken(userToken);
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        await _userRepository.SaveRefreshTokenAsync(user, refreshToken);
        
        return new LoginResponseDto(
            Token: token,
            RefreshToken: refreshToken,
            TokenExpiration: tokenExpiration
        );
    }
    
    
    
    public async Task<LoginResponseDto?> RefreshTokenAsync(string token, string refreshToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(token);
        if (principal == null) 
            return null;
        
        var email = principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email)) 
            return null;
        
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) 
            return null;
        
        if (user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow) 
            return null;
        
        var userToken = new UserToken
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role
        };
        
        var (newToken, newTokenExpiration) = _tokenService.GenerateToken(userToken);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        
        await _userRepository.SaveRefreshTokenAsync(user, newRefreshToken);
        
        return new LoginResponseDto(
            Token: newToken,
            RefreshToken: newRefreshToken,
            TokenExpiration: newTokenExpiration
        );
    }
}
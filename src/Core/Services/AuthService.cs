using System.Security.Claims;
using Core.Dtos.Login;
using Core.Dtos.Register;
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
    
    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        if (string.IsNullOrWhiteSpace(registerRequestDto.Email))
            throw new ArgumentException("Email cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(registerRequestDto.Password))
            throw new ArgumentException("Password cannot be null or empty.");
        
        if (string.IsNullOrWhiteSpace(registerRequestDto.Name))
            throw new ArgumentException("Name cannot be null or empty.");
        
        if (string.IsNullOrWhiteSpace(registerRequestDto.Role))
            throw new ArgumentException("Roles cannot be null or empty.");
        
        var existsUser = await _userRepository.ExistsByEmailAsync(registerRequestDto.Email);
        if (existsUser)
        {
            throw new InvalidOperationException("User already exists.");
        }
        
        var user = new User
        {
            Role = registerRequestDto.Role,
            Email = registerRequestDto.Email,
            Name = registerRequestDto.Name,
            RefreshToken = string.Empty,
        };
        
        var passwordHashed = _passwordHasher.HashPassword(user, registerRequestDto.Password);
        user.PasswordHash = passwordHashed;
        
        await _userRepository.AddAsync(user);

        return new RegisterResponseDto(user.Id, user.Email, user.Name, user.Role);
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
using Core.Dtos.Login;
using Core.Models;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.Services;

public class AuthService(IPasswordHasher<User> passwordHasher, 
    IUserRepository userRepository) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    
    public async Task<User?> ValidateUserCredentials(LoginRequestDto request)
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
        {
            throw new UnauthorizedAccessException("Credenciais inválidas.");
        }

        return user;
    }

    public async Task<User> GetAuthUser(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}
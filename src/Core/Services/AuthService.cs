using Core.Models;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services;

public class AuthService(IAuthRepository authRepository) : IAuthService
{
    public async Task<bool> RegisterUser(string email, string password)
    {
        if (await authRepository.ExistsByEmailAsync(email))
        {
            return false;
        }
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User()
        {
            Email = email,
            PasswordHash = passwordHash,
        };
        
        await authRepository.AddAsync(user);
        return true;
    }

    public async Task<string?> LoginUser(string email, string password)
    {
        var user = await authRepository.GetByEmailAsync(email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null;
        }

        return user.Email;
    }
}
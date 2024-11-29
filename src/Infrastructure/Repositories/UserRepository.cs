using Core.Models;
using Core.Models.Interfaces;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(AppDbContext context, ITokenConfiguration tokenConfiguration) : Repository<User>(context), IUserRepository
{
    private readonly DbSet<User> _users = context.Users;
    private readonly ITokenConfiguration _tokenConfiguration = tokenConfiguration;
    
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _users.FirstOrDefaultAsync(x => x.Email == email) != null;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task SaveRefreshTokenAsync(User user, string refreshToken)
    {
        user.RefreshToken = refreshToken;

        var refreshTokenExp = _tokenConfiguration.ExpirationInHours * 2;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(refreshTokenExp);
        
        await UpdateAsync(user);
    }

    public async Task<string?> GetRefreshTokenAsync(Guid userId)
    {
        var user = await _users
            .FirstOrDefaultAsync(u => u.Id == userId);

        return user?.RefreshToken;
    }
}
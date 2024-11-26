using Core.Models;
using Core.Models.Interfaces;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(AppDbContext context, ITokenConfiguration tokenConfiguration) : Repository<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;
    private readonly ITokenConfiguration _tokenConfiguration = tokenConfiguration;
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email) != null;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task SaveRefreshTokenAsync(User user, string refreshToken)
    {
        user.RefreshToken = refreshToken;

        var refreshTokenExp = _tokenConfiguration.ExpirationInHours * 2;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(refreshTokenExp);
        
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<string?> GetRefreshTokenAsync(Guid userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        return user?.RefreshToken;
    }
}
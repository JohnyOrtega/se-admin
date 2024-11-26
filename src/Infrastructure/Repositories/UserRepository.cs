using Core.Models;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email) != null;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task SaveRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        
        if (user == null)
        {
            throw new ArgumentException("User not found", nameof(userId));
        }
        
        user.RefreshToken = refreshToken;
        
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
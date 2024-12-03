using Core.Models;
using Core.Models.Interfaces;
using Core.Models.Request;
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
    
    public IQueryable<User> GetWithFilters(UserFilterParams filters)
    {
        var query = _users.AsQueryable();
        
        if (!string.IsNullOrEmpty(filters.Name))
            query = query.Where(m => m.Name.Contains(filters.Name));

        if (!string.IsNullOrEmpty(filters.Email))
            query = query.Where(m => m.Email.Contains(filters.Email));

        if (!string.IsNullOrEmpty(filters.Role))
            query = query.Where(m => m.Role.Contains(filters.Role));

        if (filters.CreatedAt.HasValue)
            query = query.Where(m => m.CreatedAt.Date == filters.CreatedAt.Value.Date);

        if (filters.UpdatedAt.HasValue)
            query = query.Where(m => m.UpdatedAt != null && m.UpdatedAt.Value.Date == filters.UpdatedAt.Value.Date);

        if (!string.IsNullOrEmpty(filters.UpdatedBy))
            query = query.Where(m => m.UpdatedBy == filters.UpdatedBy);
        
        return query;
    }
}
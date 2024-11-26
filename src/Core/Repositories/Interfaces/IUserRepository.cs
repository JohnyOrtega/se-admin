using Core.Models;

namespace Core.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task SaveRefreshTokenAsync(Guid userId, string refreshToken);
    Task<string?> GetRefreshTokenAsync(Guid userId);
    Task<bool> ExistsByEmailAsync(string email);
}
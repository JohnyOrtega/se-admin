using Core.Models;

namespace Core.Repositories.Interfaces;

public interface IAuthRepository : IRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByEmailAsync(string email);
}
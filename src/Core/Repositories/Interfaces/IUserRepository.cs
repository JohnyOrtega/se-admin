using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    IQueryable<User> GetWithFilters(UserFilterParams filters);
}
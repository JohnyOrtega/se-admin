using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface IAbordagemRepository : IRepository<Abordagem>
{
    IQueryable<Abordagem> GetWithFilters(AbordagemFilterParams filters);
}
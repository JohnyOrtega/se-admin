using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface IAbordagemRepository : IRepository<Abordagem>
{
    Task<List<Abordagem>> GetAllPendings();
    IQueryable<Abordagem> GetWithFilters(AbordagemFilterParams filters);
}
using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface IMapeadorRepository : IRepository<Mapeador>
{
    IQueryable<Mapeador> GetWithFilters(MapeadorFilterParams filters);
}
using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface ICemiterioRepository : IRepository<Cemiterio>
{
    IQueryable<Cemiterio> GetWithFilters(CemiterioFilterParams filters);
}
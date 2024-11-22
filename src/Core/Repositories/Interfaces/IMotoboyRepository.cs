using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface IMotoboyRepository : IRepository<Motoboy>
{
    IQueryable<Motoboy> GetWithFilters(MotoboyFilterParams filters);
}
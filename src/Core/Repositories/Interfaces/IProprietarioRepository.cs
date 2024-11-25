using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface IProprietarioRepository : IRepository<Proprietario>
{
    IQueryable<Proprietario> GetWithFilters(ProprietarioFilterParams filters);
}
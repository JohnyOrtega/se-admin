using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface IEmpresaRepository : IRepository<Empresa>
{
    IQueryable<Empresa> GetWithFilters(EmpresaFilterParams filters);
}
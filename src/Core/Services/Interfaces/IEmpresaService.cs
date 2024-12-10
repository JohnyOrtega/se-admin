using Core.Dtos.EmpresaDto;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IEmpresaService
{
    Task<Empresa> Create(Empresa empresa);
    Task<PagedResponse<Empresa>> GetWithFilters(EmpresaFilterParams filters);
    Task<Empresa> GetById(Guid id);
    Task<Empresa> UpdateAsync(EmpresaUpdateDto empresaUpdateDto);
    Task DeleteAsync(Guid id);
}
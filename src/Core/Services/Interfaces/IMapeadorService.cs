using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IMapeadorService
{
    Task<string> Create(Mapeador mapeadorDto);
    Task<PagedResponse<Mapeador>> GetWithFilters(MapeadorFilterParams filters);
    Task DeleteAsync(Guid id);
    Task<Mapeador> UpdateAsync(MapeadorDto mapeador);
}
using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface ICemiterioService
{
    Task<string> Create(Cemiterio cemiterio);
    Task<PagedResponse<Cemiterio>> GetWithFilters(CemiterioFilterParams filters);
    Task UpdateAsync(CemiterioDto cemiterioDto);
    Task DeleteAsync(Guid id);
}
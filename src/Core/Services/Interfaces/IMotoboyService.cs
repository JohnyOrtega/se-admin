using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IMotoboyService
{
    Task<string> Create(Motoboy motoboyDto);
    Task<PagedResponse<Motoboy>> GetWithFilters(MotoboyFilterParams filters);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(MotoboyDto motoboy);
}
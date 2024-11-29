using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IPedidoService
{
    Task<string> Create(Pedido pedido);
    Task<PagedResponse<Pedido>> GetWithFilters(PedidoFilterParams filters);
    Task DeleteAsync(Guid id);
    Task<Pedido> UpdateAsync(PedidoDto pedidoDto);
}
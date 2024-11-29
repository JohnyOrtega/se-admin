using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Repositories.Interfaces;

public interface IPedidoRepository : IRepository<Pedido>
{
    IQueryable<Pedido> GetWithFilters(PedidoFilterParams filters);
}
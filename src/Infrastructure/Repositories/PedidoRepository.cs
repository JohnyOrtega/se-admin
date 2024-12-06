using Core.Models;
using Core.Models.Request;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PedidoRepository(AppDbContext context) : Repository<Pedido>(context), IPedidoRepository
{
    private readonly DbSet<Pedido> _pedidos = context.Set<Pedido>();
    public IQueryable<Pedido> GetWithFilters(PedidoFilterParams filters)
    {
        var query = _pedidos.AsQueryable();
        
        return query;
    }

    public override async Task<Pedido> GetByIdAsync(Guid id)
    {
        var pedido = await _pedidos
            .Include(p => p.PedidoImoveis)
            .ThenInclude(pi => pi.Imovel)
            .FirstOrDefaultAsync(p => p.Id == id);

        return pedido ?? throw new InvalidOperationException();
    }
}
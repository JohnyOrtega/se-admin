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

        if (!string.IsNullOrEmpty(filters.Performer))
            query = query.Where(p => p.Performer.Contains(filters.Performer));
        
        if (!string.IsNullOrEmpty(filters.Expander))
            query = query.Where(p => p.Expander.Contains(filters.Expander));
        
        if (!string.IsNullOrEmpty(filters.Coordinator))
            query = query.Where(p => p.Coordinator.Contains(filters.Coordinator));
        
        if (filters.EntryDate.HasValue)
            query = query.Where(p => p.EntryDate.Date == filters.EntryDate.Value.Date);

        if (filters.DeliveryDate.HasValue)
            query = query.Where(p => p.DeliveryDate.Date == filters.DeliveryDate.Value.Date);

        if (!string.IsNullOrEmpty(filters.Client))
            query = query.Where(p => p.Client.Contains(filters.Client));
        
        if (!string.IsNullOrEmpty(filters.Order))
            query = query.Where(p => p.Order.Contains(filters.Order));
        
        if (!string.IsNullOrEmpty(filters.PropertyType))
            query = query.Where(p => p.PropertyType.Contains(filters.PropertyType));
        
        if (filters.ParkingSpaces.HasValue)
            query = query.Where(p => p.ParkingSpaces == filters.ParkingSpaces.Value);

        if (!string.IsNullOrEmpty(filters.Status))
            query = query.Where(p => p.Status.Contains(filters.Status));
       
        if (!string.IsNullOrEmpty(filters.ZeroPoint))
            query = query.Where(p => p.ZeroPoint.Contains(filters.ZeroPoint));
        
        if (!string.IsNullOrEmpty(filters.PropertyValue))
            query = query.Where(p => p.PropertyValue.Contains(filters.PropertyValue));
        
        if (filters.OnlineCreated.HasValue)
            query = query.Where(p => p.OnlineCreated.Equals(filters.OnlineCreated));
        
        if (filters.OnlineDate.HasValue)
            query = query.Where(p => p.OnlineDate.Date == filters.OnlineDate.Value.Date);
        
        if (!string.IsNullOrEmpty(filters.City))
            query = query.Where(p => p.City.Contains(filters.City));
        
        if (!string.IsNullOrEmpty(filters.State))
            query = query.Where(p => p.State.Contains(filters.State));
        
        if (filters.MappingCompleted.HasValue)
            query = query.Where(p => p.MappingCompleted.Equals(filters.MappingCompleted));

        if (!string.IsNullOrEmpty(filters.UpdatedBy))
            query = query.Where(m => m.UpdatedBy == filters.UpdatedBy);

        if (filters.CreatedAt.HasValue)
            query = query.Where(p => p.CreatedAt.Date == filters.CreatedAt.Value.Date);
        
        if (filters.UpdatedAt.HasValue)
            query = query.Where(p => p.UpdatedAt != null && p.UpdatedAt.Value.Date == filters.UpdatedAt.Value.Date);

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
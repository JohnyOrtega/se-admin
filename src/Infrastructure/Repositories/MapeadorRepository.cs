using Core.Models;
using Core.Models.Request;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MapeadorRepository(AppDbContext context) : Repository<Mapeador>(context), IMapeadorRepository
{
    private readonly DbSet<Mapeador> _mapeadores = context.Mapeadores;
    public IQueryable<Mapeador> GetWithFilters(MapeadorFilterParams filters)
    {
        var query = _mapeadores.AsQueryable();
        
        if (!string.IsNullOrEmpty(filters.Name))
            query = query.Where(m => m.Name.Contains(filters.Name));

        if (!string.IsNullOrEmpty(filters.City))
            query = query.Where(m => m.City.Contains(filters.City));
        
        if (!string.IsNullOrEmpty(filters.Zone))
            query = query.Where(m => m.Zone.Contains(filters.Zone));

        if (!string.IsNullOrEmpty(filters.Vehicle))
            query = query.Where(m => m.Vehicle.Contains(filters.Vehicle));
        
        if (!string.IsNullOrEmpty(filters.CameraType))
            query = query.Where(m => m.CameraType.Contains(filters.CameraType)); 
        
        if (!string.IsNullOrEmpty(filters.CelphoneModel))
            query = query.Where(m => m.CelphoneModel.Contains(filters.CelphoneModel));
        
        if (filters.LastMapping.HasValue)
            query = query.Where(m => m.LastMapping.Date == filters.LastMapping.Value.Date);
        
        return query;
    }
}
using Core.Models;
using Core.Models.Request;
using Core.Repositories.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class ProprietarioRepository(AppDbContext context) : Repository<Proprietario>(context), IProprietarioRepository
{
    private readonly AppDbContext _context = context;
    public IQueryable<Proprietario> GetWithFilters(ProprietarioFilterParams filters)
    {
        var query = _context.Proprietarios.AsQueryable();
        
        if (!string.IsNullOrEmpty(filters.Name))
            query = query.Where(m => m.Name.Contains(filters.Name));

        if (!string.IsNullOrEmpty(filters.City))
            query = query.Where(m => m.City.Contains(filters.City));
        
        return query;
    }
}
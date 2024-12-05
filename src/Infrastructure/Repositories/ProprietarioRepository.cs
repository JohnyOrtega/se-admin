using Core.Models;
using Core.Models.Request;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProprietarioRepository(AppDbContext context) : Repository<Proprietario>(context), IProprietarioRepository
{
    private readonly DbSet<Proprietario> _proprietarios = context.Proprietarios;
    public IQueryable<Proprietario> GetWithFilters(ProprietarioFilterParams filters)
    {
        var query = _proprietarios.AsQueryable();
    
        if (!string.IsNullOrEmpty(filters.Name))
            query = query.Where(m => m.Name.Contains(filters.Name));

        if (!string.IsNullOrEmpty(filters.Source))
            query = query.Where(m => m.Source.Contains(filters.Source));

        if (!string.IsNullOrEmpty(filters.Telephone))
            query = query.Where(m => m.Telephone.Contains(filters.Telephone));

        if (!string.IsNullOrEmpty(filters.Address))
            query = query.Where(m => m.Address.Contains(filters.Address));

        if (!string.IsNullOrEmpty(filters.Neighboor))
            query = query.Where(m => m.Neighboor.Contains(filters.Neighboor));

        if (!string.IsNullOrEmpty(filters.City))
            query = query.Where(m => m.City.Contains(filters.City));

        if (!string.IsNullOrEmpty(filters.State))
            query = query.Where(m => m.State.Contains(filters.State));

        if (!string.IsNullOrEmpty(filters.Email))
            query = query.Where(m => m.Email.Contains(filters.Email));

        if (filters.CreatedAt.HasValue)
            query = query.Where(m => m.CreatedAt.Date == filters.CreatedAt.Value.Date);

        if (filters.UpdatedAt.HasValue)
            query = query.Where(m => m.UpdatedAt != null && m.UpdatedAt.Value.Date == filters.UpdatedAt.Value.Date);

        if (!string.IsNullOrEmpty(filters.UpdatedBy))
            query = query.Where(m => m.UpdatedBy == filters.UpdatedBy);
    
        return query;
    }

    public override async Task<Proprietario> GetByIdAsync(Guid id)
    {
        return await _proprietarios
            .Include(p => p.Imoveis)
            .FirstOrDefaultAsync(p => p.Id == id) ?? throw new InvalidOperationException();
    }
}
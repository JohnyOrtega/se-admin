using Core.Models;
using Core.Models.Request;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmpresaRepository(AppDbContext context) : Repository<Empresa>(context), IEmpresaRepository
{
    private readonly DbSet<Empresa> _empresas = context.Empresas;
    public IQueryable<Empresa> GetWithFilters(EmpresaFilterParams filters)
    {
        var query = _empresas.AsQueryable();

        if (!string.IsNullOrEmpty(filters.FantasyName))
            query = query.Where(m => m.FantasyName.Contains(filters.FantasyName));

        if (!string.IsNullOrEmpty(filters.Category))
            query = query.Where(m => m.Category.Contains(filters.Category));

        if (!string.IsNullOrEmpty(filters.Telephone))
            query = query.Where(m => m.Telephone.Contains(filters.Telephone));

        if (!string.IsNullOrEmpty(filters.SocialReason))
            query = query.Where(m => m.SocialReason.Contains(filters.SocialReason));

        if (filters.CreatedAt.HasValue)
            query = query.Where(m => m.CreatedAt.Date == filters.CreatedAt.Value.Date);

        if (filters.UpdatedAt.HasValue)
            query = query.Where(m => m.UpdatedAt != null && m.UpdatedAt.Value.Date == filters.UpdatedAt.Value.Date);

        if (!string.IsNullOrEmpty(filters.UpdatedBy))
            query = query.Where(m => m.UpdatedBy == filters.UpdatedBy);

        return query;
    }

    public override async Task<Empresa> GetByIdAsync(Guid id)
    {
        return await _empresas
            .Include(i => i.Contatos)
            .FirstOrDefaultAsync(i => i.Id == id) ?? throw new InvalidOperationException();
    }
}
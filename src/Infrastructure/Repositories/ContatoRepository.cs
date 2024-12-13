using Core.Models;
using Core.Models.Request;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContatoRepository(AppDbContext context) : Repository<Contato>(context), IContatoRepository
{
    private readonly DbSet<Contato> _contatos = context.Contatos;
    public IQueryable<Contato> GetWithFilters(ContatoFilterParams filters)
    {
        var query = _contatos.AsQueryable();

        if (!string.IsNullOrEmpty(filters.Email))
            query = query.Where(m => m.Email.Contains(filters.Email));

        if (!string.IsNullOrEmpty(filters.Telephone))
            query = query.Where(m => m.Telephone.Contains(filters.Telephone));

        if (!string.IsNullOrEmpty(filters.Name))
            query = query.Where(m => m.Name.Contains(filters.Name));

        if (!string.IsNullOrEmpty(filters.Position))
            query = query.Where(m => m.Position.Contains(filters.Position));

        if (filters.CreatedAt.HasValue)
            query = query.Where(m => m.CreatedAt.Date == filters.CreatedAt.Value.Date);

        if (filters.UpdatedAt.HasValue)
            query = query.Where(m => m.UpdatedAt != null && m.UpdatedAt.Value.Date == filters.UpdatedAt.Value.Date);

        if (!string.IsNullOrEmpty(filters.UpdatedBy))
            query = query.Where(m => m.UpdatedBy == filters.UpdatedBy);

        return query;
    }

    public override async Task<Contato> GetByIdAsync(Guid id)
    {
        return await _contatos
            .Include(c => c.Abordagens)
            .FirstOrDefaultAsync(c => c.Id == id) ?? throw new InvalidOperationException();
    }
}
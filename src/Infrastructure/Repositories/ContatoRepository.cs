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

        return query;
    }

    public override async Task<Contato> GetByIdAsync(Guid id)
    {
        return await _contatos
            .Include(c => c.Abordagens)
            .FirstOrDefaultAsync(c => c.Id == id) ?? throw new InvalidOperationException();
    }
}
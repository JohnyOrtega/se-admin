using Core.Models;
using Core.Models.Request;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AbordagemRepository(AppDbContext context) : Repository<Abordagem>(context), IAbordagemRepository
{
    private readonly DbSet<Abordagem> _abordagens = context.Abordagens;
    public IQueryable<Abordagem> GetWithFilters(AbordagemFilterParams filters)
    {
        var query = _abordagens.AsQueryable().Include(a => a.Contato);

        return query;
    }

    public override async Task<Abordagem> GetByIdAsync(Guid id)
    {
        return await _abordagens
            .FirstOrDefaultAsync(c => c.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<List<Abordagem>> GetAllPendings()
    {
        var query = _abordagens.AsQueryable()
            .Include(a => a.Contato)
            .Where(a => a.NextApproachDate >= DateTime.Now.AddDays(-1))
            .OrderBy(a => a.NextApproachDate);


        return await query.ToListAsync();
    }

    public async Task<List<Abordagem>> GetPendingsByEmail(string email)
    {
        var query = _abordagens.AsQueryable()
            .Include(a => a.Contato)
            .Where(a => 
                (a.UpdatedBy == email) || 
                (string.IsNullOrEmpty(a.UpdatedBy) && a.CreatedBy == email))
            .Where(a => a.NextApproachDate >= DateTime.Now.AddDays(-1))
            .OrderBy(a => a.NextApproachDate);

        return await query.ToListAsync();
    }
}
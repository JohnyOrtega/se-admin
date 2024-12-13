using System.Linq;
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
        var query = _abordagens.AsQueryable();

        if (!string.IsNullOrEmpty(filters.Status))
            query = query.Where(m => m.Status.Contains(filters.Status));

        if (!filters.ContactAddressed == null)
            query = query.Where(m => m.ContactAddressed.Equals(filters.ContactAddressed));

        if (!string.IsNullOrEmpty(filters.Comment))
            query = query.Where(m => m.Comment.Contains(filters.Comment));

        if (!string.IsNullOrEmpty(filters.Telephone))
            query = query.Where(m => m.Telephone.Contains(filters.Telephone));

        if (!string.IsNullOrEmpty(filters.ApproachType))
            query = query.Where(m => m.ApproachType.Contains(filters.ApproachType));

        if (filters.LastApproachDate.HasValue)
            query = query.Where(m => m.LastApproachDate.Date == filters.LastApproachDate.Value.Date);

        if (filters.NextApproachDate.HasValue)
            query = query.Where(m => m.NextApproachDate.Date == filters.NextApproachDate.Value.Date);

        if (filters.CreatedAt.HasValue)
            query = query.Where(m => m.CreatedAt.Date == filters.CreatedAt.Value.Date);

        if (filters.UpdatedAt.HasValue)
            query = query.Where(m => m.UpdatedAt != null && m.UpdatedAt.Value.Date == filters.UpdatedAt.Value.Date);

        if (!string.IsNullOrEmpty(filters.UpdatedBy))
            query = query.Where(m => m.UpdatedBy == filters.UpdatedBy);

        return query.Include(a => a.Contato);
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
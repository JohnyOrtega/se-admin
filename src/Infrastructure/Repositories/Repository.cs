using Core.Models;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<TEntity>(AppDbContext context) : IRepository<TEntity>
    where TEntity : Entity
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual IQueryable<TEntity> GetQueryWithPagination(int pageNumber, int pageSize)
    {
        var query = _dbSet.AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return query;
    }

    public virtual async Task<int> GetTotal()
    {
        return await _dbSet.AsNoTracking().CountAsync();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Modified)
        {
            await _context.SaveChangesAsync();
        }

        return entity;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) != null;
    }
}
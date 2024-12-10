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

        return query;
    }

    public override async Task<Empresa> GetByIdAsync(Guid id)
    {
        return await _empresas
            .Include(i => i.Contatos)
            .FirstOrDefaultAsync(i => i.Id == id) ?? throw new InvalidOperationException();
    }
}
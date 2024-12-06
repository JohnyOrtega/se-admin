using Core.Models;
using Core.Models.Request;
using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ImovelRepository(AppDbContext context) : Repository<Imovel>(context), IImovelRepository
{
    private readonly DbSet<Imovel> _imoveis = context.Imoveis;
    public IQueryable<Imovel> GetWithFilters(ImovelFilterParams filters)
    {
        var query = _imoveis.AsQueryable();
        
        if (!string.IsNullOrEmpty(filters.Address))
            query = query.Where(c => c.Address.Contains(filters.Address));

        if (!string.IsNullOrEmpty(filters.Neighborhood))
            query = query.Where(c => c.Neighborhood.Contains(filters.Neighborhood));

        if (!string.IsNullOrEmpty(filters.City))
            query = query.Where(c => c.City.Contains(filters.City));

        if (!string.IsNullOrEmpty(filters.State))
            query = query.Where(c => c.State.Contains(filters.State));

        if (!string.IsNullOrEmpty(filters.Zone))
            query = query.Where(c => c.Zone.Contains(filters.Zone));

        if (!string.IsNullOrEmpty(filters.PropertyProfile))
            query = query.Where(c => c.PropertyProfile.Contains(filters.PropertyProfile));

        if (!string.IsNullOrEmpty(filters.Availability))
            query = query.Where(c => c.Availability.Contains(filters.Availability));

        if (filters.MinRentValue.HasValue)
            query = query.Where(c => c.RentValue >= filters.MinRentValue.Value);

        if (filters.MaxRentValue.HasValue)
            query = query.Where(c => c.RentValue <= filters.MaxRentValue.Value);

        if (filters.MinSaleValue.HasValue)
            query = query.Where(c => c.SaleValue >= filters.MinSaleValue.Value);

        if (filters.MaxSaleValue.HasValue)
            query = query.Where(c => c.SaleValue <= filters.MaxSaleValue.Value);

        if (filters.MinIptuValue.HasValue)
            query = query.Where(c => c.IptuValue >= filters.MinIptuValue.Value);

        if (filters.MaxIptuValue.HasValue)
            query = query.Where(c => c.IptuValue <= filters.MaxIptuValue.Value);

        if (filters.MinSearchMeterage.HasValue)
            query = query.Where(c => c.SearchMeterage >= filters.MinSearchMeterage.Value);

        if (filters.MaxSearchMeterage.HasValue)
            query = query.Where(c => c.SearchMeterage <= filters.MaxSearchMeterage.Value);

        if (filters.MinTotalArea.HasValue)
            query = query.Where(c => c.TotalArea >= filters.MinTotalArea.Value);

        if (filters.MaxTotalArea.HasValue)
            query = query.Where(c => c.TotalArea <= filters.MaxTotalArea.Value);

        if (!string.IsNullOrEmpty(filters.RealEstate))
            query = query.Where(c => c.RealEstate.Contains(filters.RealEstate));

        if (filters.ProprietarioId.HasValue)
            query = query.Where(c => c.ProprietarioId == filters.ProprietarioId.Value);

        if (filters.CreatedAt.HasValue)
            query = query.Where(c => c.CreatedAt.Date == filters.CreatedAt.Value.Date);

        if (filters.UpdatedAt.HasValue)
            query = query.Where(c => c.UpdatedAt != null && c.UpdatedAt.Value.Date == filters.UpdatedAt.Value.Date);
    
        return query;
    }

    public override async Task<Imovel> GetByIdAsync(Guid id)
    {
        return await _imoveis
            .Include(i => i.Proprietario)
            .FirstOrDefaultAsync(i => i.Id == id) ?? throw new InvalidOperationException();
    }
}
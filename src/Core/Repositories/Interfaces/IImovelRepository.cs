using Core.Models;
using Core.Models.Request;

namespace Core.Repositories.Interfaces;

public interface IImovelRepository : IRepository<Imovel>
{
    IQueryable<Imovel> GetWithFilters(ImovelFilterParams filters);
}
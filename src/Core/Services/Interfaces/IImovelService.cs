using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IImovelService
{
    Task<string> Create(Imovel imovel);
    Task<PagedResponse<Imovel>> GetWithFilters(ImovelFilterParams filters);
    Task<Imovel> UpdateAsync(ImovelDto imovelDto);
    Task DeleteAsync(Guid id);
}
using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IProprietarioService
{
    Task<string> Create(Proprietario proprietario);
    Task<PagedResponse<Proprietario>> GetWithFilters(ProprietarioFilterParams filters);
    Task DeleteAsync(Guid id);
    Task<Proprietario> UpdateAsync(ProprietarioDto proprietarioDto);
    Task<Proprietario> GetById(Guid id);
}
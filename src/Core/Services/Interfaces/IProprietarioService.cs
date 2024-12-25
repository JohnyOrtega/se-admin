using Core.Dtos.ProprietarioDto;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IProprietarioService
{
    Task<string> Create(Proprietario proprietario);
    Task<PagedResponse<ProprietarioDto>> GetWithFilters(ProprietarioFilterParams filters);
    Task DeleteAsync(Guid id);
    Task<ProprietarioDto> UpdateAsync(ProprietarioUpdateDto proprietarioDto);
    Task<ProprietarioDto> GetById(Guid id);
}
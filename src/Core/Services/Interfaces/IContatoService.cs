using Core.Dtos.ContatoDto;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IContatoService
{
    Task<string> Create(Contato contato);
    Task<PagedResponse<Contato>> GetWithFilters(ContatoFilterParams filters);
    Task<Contato> GetById(Guid id);
    Task<Contato> UpdateAsync(ContatoUpdateDto contatoUpdateDto);
    Task DeleteAsync(Guid id);
}
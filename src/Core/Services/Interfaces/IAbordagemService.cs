using Core.Dtos.AbordagemDto;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Core.Services.Interfaces;

public interface IAbordagemService
{
    Task<Abordagem> Create(Abordagem abordagem);
    Task<PagedResponse<Abordagem>> GetWithFilters(AbordagemFilterParams filters);
    Task<Abordagem> GetById(Guid id);
    Task<Abordagem> UpdateAsync(AbordagemUpdateDto abordagemUpdateDto);
    Task DeleteAsync(Guid id);
}
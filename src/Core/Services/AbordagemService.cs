using AutoMapper;
using Core.Dtos.AbordagemDto;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class AbordagemService(
    IMapper mapper,
    IAbordagemRepository abordagemRepository) : IAbordagemService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAbordagemRepository _abordagemRepository = abordagemRepository;

    public async Task<Abordagem> Create(Abordagem abordagem)
    {
        var existsAbordagem = await _abordagemRepository.ExistsAsync(abordagem.Id);
        if (existsAbordagem)
        {
            throw new Exception("Abordagem alreay exists.");
        }

        var abordagemCreated = await _abordagemRepository.AddAsync(abordagem);

        return abordagemCreated;
    }

    public async Task<PagedResponse<Abordagem>> GetWithFilters(AbordagemFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;

        var query = _abordagemRepository.GetWithFilters(filters);

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<Abordagem>()
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalItems = totalItems
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        var existsAbordagem = await _abordagemRepository.ExistsAsync(id);
        if (!existsAbordagem)
        {
            throw new Exception("Abordagem is not found.");
        }

        await _abordagemRepository.DeleteAsync(id);
    }

    public async Task<Abordagem> GetById(Guid id)
    {
        return await _abordagemRepository.GetByIdAsync(id);
    }

    public async Task<Abordagem> UpdateAsync(AbordagemUpdateDto abordagemUpdateDto)
    {
        var abordagem = await _abordagemRepository.GetByIdAsync(abordagemUpdateDto.Id);
        if (abordagem == null)
        {
            throw new Exception("Abordagem is not found.");
        }

        _mapper.Map(abordagemUpdateDto, abordagem);

        return await _abordagemRepository.UpdateAsync(abordagem);
    }

    public async Task<List<Abordagem>> GetAllPendings()
    {
        return await _abordagemRepository.GetAllPendings();
    }
}

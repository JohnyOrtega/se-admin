using AutoMapper;
using Core.Dtos.ContatoDto;
using Core.Exceptions;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class ContatoService(
    IMapper mapper,
    IContatoRepository contatoRepository) : IContatoService
{
    private readonly IMapper _mapper = mapper;   
    private readonly IContatoRepository _contatoRepository = contatoRepository;

    public async Task<Contato> Create(Contato contato)
    {
        var existsContato = await _contatoRepository.ExistsAsync(contato.Id);
        if (existsContato)
        {
            throw AlreadyExistsException.For("Contato", contato.Id);
        }

        var contatoCreated = await _contatoRepository.AddAsync(contato);

        return contatoCreated;
    }

    public async Task<PagedResponse<Contato>> GetWithFilters(ContatoFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;

        var query = _contatoRepository.GetWithFilters(filters);

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<Contato>()
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
        var existsContato = await _contatoRepository.ExistsAsync(id);
        if (!existsContato)
        {
            throw NotFoundException.For("Contato", id);
        }

        await _contatoRepository.DeleteAsync(id);
    }

    public async Task<Contato> GetById(Guid id)
    {
        return await _contatoRepository.GetByIdAsync(id);
    }

    public async Task<Contato> UpdateAsync(ContatoUpdateDto contatoUpdateDto)
    {
        var contato = await _contatoRepository.GetByIdAsync(contatoUpdateDto.Id) ?? throw NotFoundException.For("Contato", contatoUpdateDto.Id);
        _mapper.Map(contatoUpdateDto, contato);

        return await _contatoRepository.UpdateAsync(contato);
    }
}

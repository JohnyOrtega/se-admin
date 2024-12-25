using AutoMapper;
using Core.Dtos.ProprietarioDto;
using Core.Exceptions;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class ProprietarioService(IProprietarioRepository proprietarioRepository, IMapper mapper) : IProprietarioService
{
    private readonly IProprietarioRepository _proprietarioRepository = proprietarioRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<string> Create(Proprietario proprietario)
    { 
        var existsProprietario = await _proprietarioRepository.ExistsAsync(proprietario.Id);
        if (existsProprietario)
        {
            throw AlreadyExistsException.For("Proprietario", proprietario.Email);
        }

        await _proprietarioRepository.AddAsync(proprietario);
        
        return proprietario.Id.ToString();
    }

    public async Task<PagedResponse<ProprietarioDto>> GetWithFilters(ProprietarioFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;
        
        var query = _proprietarioRepository.GetWithFilters(filters);
        
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await query
            .Select(p => new
            {
                Proprietario = p,
                ImoveisCount = p.Imoveis.Count()
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var proprietariosDto = items.Select(item =>
        {
            var dto = _mapper.Map<ProprietarioDto>(item.Proprietario);
            dto.ImoveisCount = item.ImoveisCount;
            return dto;
        }).ToList();

        return new PagedResponse<ProprietarioDto>()
        {
            Items = proprietariosDto,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalItems = totalItems
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        var existsProprietario = await _proprietarioRepository.ExistsAsync(id);
        if (!existsProprietario)
        {
            throw NotFoundException.For("Proprietario", id);
        }

        await _proprietarioRepository.DeleteAsync(id);
    }

    public async Task<ProprietarioDto> UpdateAsync(ProprietarioUpdateDto proprietarioUpdateDto)
    {
        var proprietario = await _proprietarioRepository.GetByIdAsync(proprietarioUpdateDto.Id) ?? throw NotFoundException.For("Proprietario", proprietarioUpdateDto.Id);
        _mapper.Map(proprietarioUpdateDto, proprietario);

        var proprietarioUpdated = await _proprietarioRepository.UpdateAsync(proprietario);

        return _mapper.Map<ProprietarioDto>(proprietarioUpdated);
    }

    public async Task<ProprietarioDto> GetById(Guid id)
    {
        var proprietario = await _proprietarioRepository.GetByIdAsync(id);

        return _mapper.Map<ProprietarioDto>(proprietario);
    }
}
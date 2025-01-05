using AutoMapper;
using Core.Dtos.MapeadorDto;
using Core.Exceptions;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class MapeadorService(IMapeadorRepository mapeadorRepository, IMapper mapper) : IMapeadorService
{
    private readonly IMapeadorRepository _mapeadorRepository = mapeadorRepository;
    
    public async Task<string> Create(Mapeador mapeador)
    { 
        var existsMapeador = await _mapeadorRepository.ExistsAsync(mapeador.Id);
        if (existsMapeador)
        {
            throw AlreadyExistsException.For("Mapeador", mapeador.Id);
        }

        await _mapeadorRepository.AddAsync(mapeador);
        
        return mapeador.Id.ToString();
    }

    public async Task<PagedResponse<Mapeador>> GetWithFilters(MapeadorFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;
        
        var query = _mapeadorRepository.GetWithFilters(filters);
        
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResponse<Mapeador>()
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
        var existsMapeador = await _mapeadorRepository.ExistsAsync(id);
        if (!existsMapeador)
        {
            throw NotFoundException.For("Mapeador", id);
        }

        await _mapeadorRepository.DeleteAsync(id);
    }

    public async Task<Mapeador> UpdateAsync(MapeadorUpdateDto mapeadorDto)
    {
        var mapeador = await _mapeadorRepository.GetByIdAsync(mapeadorDto.Id) ?? throw NotFoundException.For("Mapeador", mapeadorDto.Id);
        mapper.Map(mapeadorDto, mapeador);
        
        var mapeadorUpdated = await _mapeadorRepository.UpdateAsync(mapeador);
        return mapeadorUpdated;
    }

    public async Task<Mapeador> GetById(Guid id)
    {
        return await _mapeadorRepository.GetByIdAsync(id); 
    }
}
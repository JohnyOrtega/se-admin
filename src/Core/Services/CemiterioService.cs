using AutoMapper;
using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class CemiterioService(ICemiterioRepository cemiterioRepository, IMapper mapper) : ICemiterioService
{
    private readonly ICemiterioRepository _cemiterioRepository = cemiterioRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<string> Create(Cemiterio cemiterio)
    { 
        var existsCemiterio = await _cemiterioRepository.ExistsAsync(cemiterio.Id);
        if (existsCemiterio)
        {
            return "Cemiterio is already exists";
        }

        await _cemiterioRepository.AddAsync(cemiterio);
        
        return cemiterio.Id.ToString();
    }

    public async Task<PagedResponse<Cemiterio>> GetWithFilters(CemiterioFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;
        
        var query = _cemiterioRepository.GetWithFilters(filters);
        
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResponse<Cemiterio>()
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
        var existsCemiterio = await _cemiterioRepository.ExistsAsync(id);
        if (!existsCemiterio)
        {
            throw new Exception("Cemiterio is not found.");
        }

        await _cemiterioRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(CemiterioDto cemiterioDto)
    {
        var cemiterio = await _cemiterioRepository.GetByIdAsync(cemiterioDto.Id);
        if (cemiterio == null)
        {
            throw new Exception("Cemiterio is not found.");
        }
        
        _mapper.Map(cemiterioDto, cemiterio);
        
        await _cemiterioRepository.UpdateAsync(cemiterio);
    }
}
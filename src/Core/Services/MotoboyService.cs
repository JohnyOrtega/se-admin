using AutoMapper;
using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class MotoboyService(IMotoboyRepository motoboyRepository, IMapper mapper) : IMotoboyService
{
    private readonly IMotoboyRepository _motoboyRepository = motoboyRepository;
    
    public async Task<string> Create(Motoboy motoboy)
    { 
        var existsMotoboy = await _motoboyRepository.ExistsAsync(motoboy.Id);
        if (existsMotoboy)
        {
            return "Motoboy is already exists";
        }

        await _motoboyRepository.AddAsync(motoboy);
        
        return motoboy.Id.ToString();
    }

    public async Task<PagedResponse<Motoboy>> GetWithFilters(MotoboyFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;
        
        var query = _motoboyRepository.GetWithFilters(filters);
        
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResponse<Motoboy>()
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
        var existsMotoboy = await _motoboyRepository.ExistsAsync(id);
        if (!existsMotoboy)
        {
            throw new Exception("Motoboy is not found.");
        }

        await _motoboyRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(MotoboyDto motoboyDto)
    {
        var motoboy = await _motoboyRepository.GetByIdAsync(motoboyDto.Id);
        if (motoboy == null)
        {
            throw new Exception("Motoboy is not found.");
        }
        
        mapper.Map(motoboyDto, motoboy);
        
        await _motoboyRepository.UpdateAsync(motoboy);
    }
}
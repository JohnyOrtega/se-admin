using AutoMapper;
using Core.Dtos;
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
        var existsMotoboy = await _mapeadorRepository.ExistsAsync(mapeador.Id);
        if (existsMotoboy)
        {
            return "Motoboy is already exists";
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
        var existsMotoboy = await _mapeadorRepository.ExistsAsync(id);
        if (!existsMotoboy)
        {
            throw new Exception("Motoboy is not found.");
        }

        await _mapeadorRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(MapeadorDto mapeadorDto)
    {
        var motoboy = await _mapeadorRepository.GetByIdAsync(mapeadorDto.Id);
        if (motoboy == null)
        {
            throw new Exception("Motoboy is not found.");
        }
        
        mapper.Map(mapeadorDto, motoboy);
        
        await _mapeadorRepository.UpdateAsync(motoboy);
    }
}
using AutoMapper;
using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class ImovelService(IImovelRepository imovelRepository, IMapper mapper) : IImovelService
{
    private readonly IImovelRepository _imovelRepository = imovelRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<string> Create(Imovel imovel)
    { 
        var existsImovel = await _imovelRepository.ExistsAsync(imovel.Id);
        if (existsImovel)
        {
            return "Imovel is already exists";
        }

        await _imovelRepository.AddAsync(imovel);
        
        return imovel.Id.ToString();
    }

    public async Task<PagedResponse<Imovel>> GetWithFilters(ImovelFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;
        
        var query = _imovelRepository.GetWithFilters(filters);
        
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResponse<Imovel>()
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
        var existsImovel = await _imovelRepository.ExistsAsync(id);
        if (!existsImovel)
        {
            throw new Exception("Imovel is not found.");
        }

        await _imovelRepository.DeleteAsync(id);
    }

    public async Task<Imovel> UpdateAsync(ImovelDto imovelDto)
    {
        var imovel = await _imovelRepository.GetByIdAsync(imovelDto.Id);
        if (imovel == null)
        {
            throw new Exception("Imovel is not found.");
        }
        
        _mapper.Map(imovelDto, imovel);
        
        return await _imovelRepository.UpdateAsync(imovel);
    }
}
using AutoMapper;
using Core.Dtos;
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
    
    public async Task<string> Create(Proprietario proprietario)
    { 
        var existsProprietario = await _proprietarioRepository.ExistsAsync(proprietario.Id);
        if (existsProprietario)
        {
            return "Proprietario is already exists";
        }

        await _proprietarioRepository.AddAsync(proprietario);
        
        return proprietario.Id.ToString();
    }

    public async Task<PagedResponse<Proprietario>> GetWithFilters(ProprietarioFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;
        
        var query = _proprietarioRepository.GetWithFilters(filters);
        
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResponse<Proprietario>()
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
        var existsProprietario = await _proprietarioRepository.ExistsAsync(id);
        if (!existsProprietario)
        {
            throw new Exception("Proprietario is not found.");
        }

        await _proprietarioRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(ProprietarioDto proprietarioDto)
    {
        var proprietario = await _proprietarioRepository.GetByIdAsync(proprietarioDto.Id);
        if (proprietario == null)
        {
            throw new Exception("Motoboy is not found.");
        }
        
        mapper.Map(proprietarioDto, proprietario);
        
        await _proprietarioRepository.UpdateAsync(proprietario);
    }
}
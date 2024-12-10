using AutoMapper;
using Core.Dtos.EmpresaDto;
using Core.Exceptions;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class EmpresaService(
    IMapper mapper,
    IEmpresaRepository empresaRepository) : IEmpresaService
{
    private readonly IMapper _mapper = mapper;   
    private readonly IEmpresaRepository _empresaRepository = empresaRepository;

    public async Task<Empresa> Create(Empresa empresa)
    {
        var existsEmpresa = await _empresaRepository.ExistsAsync(empresa.Id);
        if (existsEmpresa)
        {
            throw new Exception("Empresa already exists");
        }

        var empresaCreated = await _empresaRepository.AddAsync(empresa);

        return empresaCreated;
    }

    public async Task<PagedResponse<Empresa>> GetWithFilters(EmpresaFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;

        var query = _empresaRepository.GetWithFilters(filters);

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<Empresa>()
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
        var existsEmpresa = await _empresaRepository.ExistsAsync(id);
        if (!existsEmpresa)
        {
            throw NotFoundException.For("Empresa", id);
        }

        await _empresaRepository.DeleteAsync(id);
    }

    public async Task<Empresa> GetById(Guid id)
    {
        return await _empresaRepository.GetByIdAsync(id);
    }

    public async Task<Empresa> UpdateAsync(EmpresaUpdateDto empresaUpdateDto)
    {
        var empresa = await _empresaRepository.GetByIdAsync(empresaUpdateDto.Id) ?? throw new Exception("Empresa is not found.");

        _mapper.Map(empresaUpdateDto, empresa);

        return await _empresaRepository.UpdateAsync(empresa);
    }
}

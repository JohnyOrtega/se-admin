using AutoMapper;
using Core.Dtos.Pedido;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class PedidoService(IPedidoRepository pedidoRepository, IMapper mapper) : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<string> Create(Pedido pedido)
    { 
        var existsPedido = await _pedidoRepository.ExistsAsync(pedido.Id);
        if (existsPedido)
        {
            return "Pedido is already exists";
        }

        await _pedidoRepository.AddAsync(pedido);
        
        return pedido.Id.ToString();
    }

    public async Task<PagedResponse<Pedido>> GetWithFilters(PedidoFilterParams filters)
    {
        var pageNumber = filters.PageNumber;
        var pageSize = filters.PageSize;
        
        var query = _pedidoRepository.GetWithFilters(filters);
        
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResponse<Pedido>()
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
        var existsPedido = await _pedidoRepository.ExistsAsync(id);
        if (!existsPedido)
        {
            throw new Exception("Pedido is not found.");
        }

        await _pedidoRepository.DeleteAsync(id);
    }

    public async Task<Pedido> UpdateAsync(PedidoUpdateDto pedidoDto)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(pedidoDto.Id) ?? throw new Exception("Pedido is not found.");
        _mapper.Map(pedidoDto, pedido);
        
        return await _pedidoRepository.UpdateAsync(pedido);
    }

    public async Task<Pedido> GetById(Guid id)
    {
        return await _pedidoRepository.GetByIdAsync(id);
    }
}
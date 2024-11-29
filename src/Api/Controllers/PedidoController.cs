using AutoMapper;
using Core.Dtos;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController(IPedidoService pedidoService, IMapper mapper) : ControllerBase
{
   private readonly IPedidoService _pedidoService = pedidoService;
   private readonly IMapper _mapper = mapper;

   [HttpGet]
   public async Task<ActionResult<PagedResponse<PedidoDto>>> GetAllWithPagination(
      [FromQuery] PedidoFilterParams filters)
   {
      var pedido = await _pedidoService.GetWithFilters(filters);
        
      return Ok(pedido);
   }
    
   [HttpPost]
   public async Task<ActionResult> Create([FromBody] PedidoDto pedidoDto)
   {
      var pedido = _mapper.Map<Pedido>(pedidoDto);
        
      var id = await _pedidoService.Create(pedido);
        
      return Created($"api/pedido/Create", id);
   }

   [HttpPut("{id:guid}")]
   public async Task<ActionResult> Update([FromBody] PedidoDto pedidoDto, Guid id)
   {
      if (pedidoDto.Id != id)
      {
         return BadRequest("Ids do not match.");
      }
        
      await _pedidoService.UpdateAsync(pedidoDto);
      return NoContent();
   }
    
   [HttpDelete("{id:guid}")]
   public async Task<ActionResult> Delete(Guid id)
   {
      await _pedidoService.DeleteAsync(id);
      return NoContent();
   }
}
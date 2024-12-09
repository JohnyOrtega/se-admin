using Api.Attributes;
using AutoMapper;
using Core.Dtos.Pedido;
using Core.Models;
using Core.Models.Request;
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
   public async Task<ActionResult> GetAllWithPagination(
      [FromQuery] PedidoFilterParams filters)
   {
      var pedido = await _pedidoService.GetWithFilters(filters);
        
      return Ok(pedido);
   }
   
   [HttpGet("{id:guid}")]
   public async Task<ActionResult> GetById(Guid id)
   {
      var pedido = await _pedidoService.GetById(id);
        
      return Ok(pedido);
   }
   
   [HttpPost]
   [AuthorizeRole("Admin", "Moderador")]
   public async Task<ActionResult> Create([FromBody] PedidoCreateDto pedidoCreateDto)
   {
      var pedido = _mapper.Map<Pedido>(pedidoCreateDto);
        
      var id = await _pedidoService.Create(pedido);
        
      return Created($"api/pedido/Create", id);
   }

   [HttpPut("{id:guid}")]
   [AuthorizeRole("Admin", "Moderador")]
   public async Task<ActionResult> Update([FromBody] PedidoUpdateDto pedidoUpdateDto, Guid id)
   {
      if (pedidoUpdateDto.Id != id)
      {
         return BadRequest("Ids do not match.");
      }
        
      await _pedidoService.UpdateAsync(pedidoUpdateDto);
      return NoContent();
   }
    
   [HttpDelete("{id:guid}")]
   [AuthorizeRole("Admin", "Moderador")]
   public async Task<ActionResult> Delete(Guid id)
   {
      await _pedidoService.DeleteAsync(id);
      return NoContent();
   }
}
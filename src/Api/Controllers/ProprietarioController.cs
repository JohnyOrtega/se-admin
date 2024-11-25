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
public class ProprietarioController(IProprietarioService proprietarioService, IMapper mapper) : ControllerBase
{
    private readonly IProprietarioService _proprietarioService = proprietarioService;
    
    [HttpGet]
    public async Task<ActionResult<PagedResponse<ProprietarioDto>>> GetAllWithPagination(
        [FromQuery] ProprietarioFilterParams filters)
    {
        var proprietarios = await _proprietarioService.GetWithFilters(filters);
        
        return Ok(proprietarios);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProprietarioDto proprietarioDto)
    {
        var proprietario = mapper.Map<Proprietario>(proprietarioDto);
        
        var id = await _proprietarioService.Create(proprietario);
        
        return Created($"api/proprietario/Create", id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update([FromBody] ProprietarioDto proprietarioDto, Guid id)
    {
        if (proprietarioDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }
        
        await _proprietarioService.UpdateAsync(proprietarioDto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _proprietarioService.DeleteAsync(id);
        return NoContent();
    }
}
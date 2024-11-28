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
public class MapeadorController(IMapeadorService mapeadorService, IMapper mapper) : ControllerBase
{
    private readonly IMapeadorService _mapeadorService = mapeadorService;
    
    [HttpGet]
    public async Task<ActionResult<PagedResponse<MapeadorDto>>> GetAllWithPagination(
        [FromQuery] MapeadorFilterParams filters)
    {
        var motoboys = await _mapeadorService.GetWithFilters(filters);
        
        return Ok(motoboys);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] MapeadorDto mapeadorDto)
    {
        var motoboy = mapper.Map<Mapeador>(mapeadorDto);
        
        var id = await _mapeadorService.Create(motoboy);
        
        return Created($"api/motoboy/Create", id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update([FromBody] MapeadorDto mapeadorDto, Guid id)
    {
        if (mapeadorDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }
        
        var mapeadorUpdated = await _mapeadorService.UpdateAsync(mapeadorDto);
        return Ok(mapeadorUpdated);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mapeadorService.DeleteAsync(id);
        return NoContent();
    }
}
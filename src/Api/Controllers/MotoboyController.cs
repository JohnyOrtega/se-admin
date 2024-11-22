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
public class MotoboyController(IMotoboyService motoboyService, IMapper mapper) : ControllerBase
{
    private readonly IMotoboyService _motoboyService = motoboyService;
    
    [HttpGet]
    public async Task<ActionResult<PagedResponse<MotoboyDto>>> GetAllWithPagination(
        [FromQuery] MotoboyFilterParams filters)
    {
        var motoboys = await _motoboyService.GetWithFilters(filters);
        
        return Ok(motoboys);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] MotoboyDto motoboyDto)
    {
        var motoboy = mapper.Map<Motoboy>(motoboyDto);
        
        var id = await _motoboyService.Create(motoboy);
        
        return Created($"api/motoboy/Create", id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update([FromBody] MotoboyDto motoboyDto, Guid id)
    {
        if (motoboyDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }
        
        await _motoboyService.UpdateAsync(motoboyDto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _motoboyService.DeleteAsync(id);
        return NoContent();
    }
}
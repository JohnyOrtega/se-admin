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
public class CemiterioController(ICemiterioService cemiterioService, IMapper mapper) : ControllerBase
{
    private readonly ICemiterioService _cemiterioService = cemiterioService;
    private readonly IMapper _mapper = mapper;
    
    [HttpGet]
    public async Task<ActionResult<PagedResponse<CemiterioDto>>> GetAllWithPagination(
        [FromQuery] CemiterioFilterParams filters)
    {
        var cemiterio = await _cemiterioService.GetWithFilters(filters);
        
        return Ok(cemiterio);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CemiterioDto cemiterioDto)
    {
        var cemiterio = _mapper.Map<Cemiterio>(cemiterioDto);
        
        var id = await _cemiterioService.Create(cemiterio);
        
        return Created($"api/cemiterio/Create", id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update([FromBody] CemiterioDto cemiterioDto, Guid id)
    {
        if (cemiterioDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }
        
        await _cemiterioService.UpdateAsync(cemiterioDto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _cemiterioService.DeleteAsync(id);
        return NoContent();
    }
}
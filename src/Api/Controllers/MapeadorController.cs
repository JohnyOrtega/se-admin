using Api.Attributes;
using AutoMapper;
using Core.Dtos.MapeadorDto;
using Core.Models;
using Core.Models.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MapeadorController(IMapeadorService mapeadorService, IMapper mapper) : ControllerBase
{
    private readonly IMapeadorService _mapeadorService = mapeadorService;

    [HttpGet]
    public async Task<ActionResult> GetAllWithPagination(
        [FromQuery] MapeadorFilterParams filters)
    {
        var motoboys = await _mapeadorService.GetWithFilters(filters);

        return Ok(motoboys);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetAllWithPagination(Guid id)
    {
        var mapeador = await _mapeadorService.GetById(id);

        return Ok(mapeador);
    }

    [HttpPost]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Create([FromBody] MapeadorCreateDto mapeadorCreateDto)
    {
        var mapeador = mapper.Map<Mapeador>(mapeadorCreateDto);

        var id = await _mapeadorService.Create(mapeador);

        return Created($"api/mapeador/Create", id);
    }

    [HttpPut("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Update([FromBody] MapeadorUpdateDto mapeadorUpdateDto, Guid id)
    {
        if (mapeadorUpdateDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }

        var mapeadorUpdated = await _mapeadorService.UpdateAsync(mapeadorUpdateDto);
        return Ok(mapeadorUpdated);
    }

    [HttpDelete("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mapeadorService.DeleteAsync(id);
        return NoContent();
    }
}
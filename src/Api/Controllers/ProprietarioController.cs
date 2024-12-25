using Api.Attributes;
using AutoMapper;
using Core.Dtos.ProprietarioDto;
using Core.Models;
using Core.Models.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProprietarioController(IProprietarioService proprietarioService, IMapper mapper) : ControllerBase
{
    private readonly IProprietarioService _proprietarioService = proprietarioService;

    [HttpGet]
    public async Task<ActionResult> GetAllWithPagination(
        [FromQuery] ProprietarioFilterParams filters)
    {
        var proprietarios = await _proprietarioService.GetWithFilters(filters);

        return Ok(proprietarios);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var proprietario = await _proprietarioService.GetById(id);
        return Ok(proprietario);
    }

    [HttpPost]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Create([FromBody] ProprietarioCreateDto proprietarioCreateDto)
    {
        var proprietario = mapper.Map<Proprietario>(proprietarioCreateDto);

        var id = await _proprietarioService.Create(proprietario);

        return Created($"api/proprietario/Create", id);
    }

    [HttpPut("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Update([FromBody] ProprietarioUpdateDto proprietarioUpdateDto, Guid id)
    {
        if (proprietarioUpdateDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }

        var proprietarioUpdated = await _proprietarioService.UpdateAsync(proprietarioUpdateDto);
        return Ok(proprietarioUpdated);
    }

    [HttpDelete("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _proprietarioService.DeleteAsync(id);
        return NoContent();
    }
}
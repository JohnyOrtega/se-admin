using Api.Attributes;
using AutoMapper;
using Core.Dtos.ContatoDto;
using Core.Models;
using Core.Models.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContatoController(IContatoService contatoService, IMapper mapper) : ControllerBase
{
    private readonly IContatoService _contatoService = contatoService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult> GetAllWithPagination(
       [FromQuery] ContatoFilterParams filters)
    {
        var contato = await _contatoService.GetWithFilters(filters);

        return Ok(contato);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var contato = await _contatoService.GetById(id);

        return Ok(contato);
    }

    [HttpPost]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Create([FromBody] ContatoCreateDto contatoCreateDto)
    {
        var contato = _mapper.Map<Contato>(contatoCreateDto);

        var id = await _contatoService.Create(contato);

        return Created($"api/contato/Create", id);
    }

    [HttpPut("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Update([FromBody] ContatoUpdateDto contatoUpdateDto, Guid id)
    {
        if (contatoUpdateDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }

        await _contatoService.UpdateAsync(contatoUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _contatoService.DeleteAsync(id);
        return NoContent();
    }
}
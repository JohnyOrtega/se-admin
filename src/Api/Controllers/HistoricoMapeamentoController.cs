using Api.Attributes;
using AutoMapper;
using Core.Dtos.HistoricoMapeamentoDto;
using Core.Models;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoricoMapeamentoController(IHistoricoMapeamentoService historicoMapeamentoService, IMapper mapper) : ControllerBase
{
    private readonly IHistoricoMapeamentoService _historicoMapeamentoService = historicoMapeamentoService;
    private readonly IMapper _mapper = mapper;

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var historicoMapeamento = await _historicoMapeamentoService.GetById(id);

        return Ok(historicoMapeamento);
    }

    [HttpPost]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Create([FromBody] HistoricoMapeamentoCreateDto historicoMapeamentoCreateDto)
    {
        var historicoMapeamento = _mapper.Map<HistoricoMapeamento>(historicoMapeamentoCreateDto);

        var historicoMapeamentoCreated = await _historicoMapeamentoService.Create(historicoMapeamento);

        return Created($"api/historicoMapeamento/Create", historicoMapeamentoCreated);
    }

    [HttpPut("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Update([FromBody] HistoricoMapeamentoUpdateDto historicoMapeamentoUpdateDto, Guid id)
    {
        if (historicoMapeamentoUpdateDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }

        await _historicoMapeamentoService.UpdateAsync(historicoMapeamentoUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _historicoMapeamentoService.DeleteAsync(id);
        return NoContent();
    }
}
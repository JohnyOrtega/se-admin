using Api.Attributes;
using AutoMapper;
using Core.Dtos.AbordagemDto;
using Core.Models;
using Core.Models.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AbordagemController(IAbordagemService abordagemService, IMapper mapper) : ControllerBase
{
    private readonly IAbordagemService _abordagemService = abordagemService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult> GetAllWithPagination(
       [FromQuery] AbordagemFilterParams filters)
    {
        var abordagem = await _abordagemService.GetWithFilters(filters);

        return Ok(abordagem);
    }

    [HttpGet("pendings")]
    public async Task<ActionResult> GetAllPendings()
    {
        var abordagens = await _abordagemService.GetAllPendings();
        return Ok(abordagens);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var abordagem = await _abordagemService.GetById(id);

        return Ok(abordagem);
    }

    [HttpPost]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Create([FromBody] AbordagemCreateDto abordagemCreateDto)
    {
        var abordagem = _mapper.Map<Abordagem>(abordagemCreateDto);

        var abordagemCreated = await _abordagemService.Create(abordagem);

        return Created($"api/abordagem/Create", abordagemCreated);
    }

    [HttpPut("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Update([FromBody] AbordagemUpdateDto abordagemUpdateDto, Guid id)
    {
        if (abordagemUpdateDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }

        await _abordagemService.UpdateAsync(abordagemUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _abordagemService.DeleteAsync(id);
        return NoContent();
    }
}
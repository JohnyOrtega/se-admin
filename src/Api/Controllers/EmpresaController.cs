using Api.Attributes;
using AutoMapper;
using Core.Dtos.EmpresaDto;
using Core.Models;
using Core.Models.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController(IEmpresaService empresaService, IMapper mapper) : ControllerBase
{
    private readonly IEmpresaService _empresaService = empresaService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult> GetAllWithPagination(
       [FromQuery] EmpresaFilterParams filters)
    {
        var empresa = await _empresaService.GetWithFilters(filters);

        return Ok(empresa);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var empresa = await _empresaService.GetById(id);

        return Ok(empresa);
    }

    [HttpPost]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Create([FromBody] EmpresaCreateDto empresaCreateDto)
    {
        var empresa = _mapper.Map<Empresa>(empresaCreateDto);

        var id = await _empresaService.Create(empresa);

        return Created($"api/empresa/Create", id);
    }

    [HttpPut("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Update([FromBody] EmpresaUpdateDto empresaUpdateDto, Guid id)
    {
        if (empresaUpdateDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }

        await _empresaService.UpdateAsync(empresaUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _empresaService.DeleteAsync(id);
        return NoContent();
    }
}
using Api.Attributes;
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
public class ImovelController(IImovelService imovelService, IMapper mapper) : ControllerBase
{
    private readonly IImovelService _imovelService = imovelService;
    private readonly IMapper _mapper = mapper;
    
    [HttpGet]
    public async Task<ActionResult> GetAllWithPagination(
        [FromQuery] ImovelFilterParams filters)
    {
        var imovel = await _imovelService.GetWithFilters(filters);
        
        return Ok(imovel);
    }
    
    [HttpPost]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Create([FromBody] ImovelDto imovelDto)
    {
        var imovel = _mapper.Map<Imovel>(imovelDto);
        
        var id = await _imovelService.Create(imovel);
        
        return Created($"api/imovel/Create", id);
    }
    
    [HttpPut("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Update([FromBody] ImovelDto imovelDto, Guid id)
    {
        if (imovelDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }
        
        await _imovelService.UpdateAsync(imovelDto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _imovelService.DeleteAsync(id);
        return NoContent();
    }
}
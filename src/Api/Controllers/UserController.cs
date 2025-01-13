using Api.Attributes;
using Core.Dtos.User;
using Core.Models.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [AuthorizeRole("Admin", "Moderador")]
    public async Task<ActionResult> GetAllWithPagination(
        [FromQuery] UserFilterParams filters)
    {
        var users = await _userService.GetWithFilters(filters);

        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    [AuthorizeRole("Admin")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var user = await _userService.GetById(id);
        return Ok(user);
    }

    [HttpPut("{id:guid}")]
    [AuthorizeRole("Admin")]
    public async Task<ActionResult> Update([FromBody] UserDto userDto, Guid id)
    {
        if (userDto.Id != id)
        {
            return BadRequest("Ids do not match.");
        }

        var proprietarioUpdated = await _userService.UpdateAsync(userDto);
        return Ok(proprietarioUpdated);
    }

    [HttpPost("register")]
    [AuthorizeRole("Admin")]
    public async Task<ActionResult> Register(
        [FromBody] UserCreationDto request)
    {
        try
        {
            var response = await _userService.RegisterAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
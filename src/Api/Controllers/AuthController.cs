using Core.Dtos;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserDto userDto)
    {
        var result = await authService.RegisterUser(userDto.Email, userDto.Password);
        if (!result)
        {
            BadRequest("Email already in use.");
        }
        
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserDto userDto)
    {
        var email = await authService.LoginUser(userDto.Email, userDto.Password);
        if (email == null)
        {
            return Unauthorized("Invalid credentials.");
        }
        
        var token = tokenService.GenerateToken(email);
        
        return Ok(new {token, email});
    }
}
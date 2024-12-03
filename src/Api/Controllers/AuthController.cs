using Core.Dtos.Login;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService; 
    
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var result = await _authService.AuthenticateAsync(loginRequestDto);
        
        if (result == null)
            return Unauthorized(new { Message = "Invalid credentials." });

        return Ok(result);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var result = await _authService.RefreshTokenAsync(
            request.Token, 
            request.RefreshToken
        );
        
        if (result == null)
            return Unauthorized(new { Message = "Invalid Refresh token." });

        return Ok(result);
    }
}
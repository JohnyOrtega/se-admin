using AutoMapper;
using Core.Dtos.Login;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, ITokenService tokenService, IMapper mapper) : ControllerBase
{
    private readonly IAuthService _authService = authService; 
    private readonly ITokenService _tokenService = tokenService;
    private readonly IMapper _mapper = mapper; 
    
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var user = await _authService.ValidateUserCredentials(loginRequestDto);
        if (user == null)
        {
            return Unauthorized();
        }
        
        var accessToken = _tokenService.GenerateAccessToken(user);
        
        return Ok(new {accessToken});
    }
}
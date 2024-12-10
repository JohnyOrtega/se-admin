using System.Security.Claims;
using Core.Dtos.Login;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, ITokenService tokenService) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly ITokenService _tokenService = tokenService;

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

        var authResponse = _tokenService.GenerateAccessToken(user);

        return Ok(authResponse);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
    {
        var principal = _tokenService.GetPrincipal(refreshTokenRequestDto.AccessToken, refreshTokenRequestDto.RefreshToken);

        var jti = principal.Claims
                .FirstOrDefault(c => c.Type == "jti")?.Value;

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentNullException(userId);
        }

        if (IsRefreshTokenValid(jti, refreshTokenRequestDto.RefreshToken))
        {
            var user = await _authService.GetAuthUser(
                Guid.Parse(userId)
            );

            var authResponse = _tokenService.GenerateAccessToken(user);
            return Ok(authResponse);
        }

        throw new SecurityTokenException("Token inválido.");
    }

    private static bool IsRefreshTokenValid(string? jti, string refreshToken)
    {
        return !string.IsNullOrEmpty(jti) &&
               !string.IsNullOrEmpty(refreshToken);
    }
}
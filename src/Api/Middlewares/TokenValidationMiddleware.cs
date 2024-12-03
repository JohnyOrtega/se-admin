using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Core.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Api.Middlewares;

public class TokenValidationMiddleware(RequestDelegate next, ITokenConfiguration tokenConfiguration)
{
    private readonly RequestDelegate _next = next;
    private readonly ITokenConfiguration _tokenConfiguration = tokenConfiguration;
    private readonly List<string> _ignoreRoutes = ["/api/auth/login", "/api/auth/logout", "/api/auth/refresh"];
    
    public async Task InvokeAsync(HttpContext context)
    {
        if (_ignoreRoutes.Contains(context.Request.Path))
        {
            await _next(context);
            return;
        }

        var accessToken = context.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
        
        if (string.IsNullOrEmpty(accessToken))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { message = "No token provided" });
            return;
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_tokenConfiguration.SecretKey);

            tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _tokenConfiguration.Issuer,
                ValidateAudience = true,
                ValidAudience = _tokenConfiguration.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out _);
        }
        catch (SecurityTokenExpiredException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { 
                message = "Token has expired", 
                requiresRefresh = true 
            });
            return;
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _next(context);
    }
}
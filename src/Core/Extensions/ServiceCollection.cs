using Core.Services;
using Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services
            .AddScoped<IMotoboyService, MotoboyService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}
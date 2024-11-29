using Core.Models;
using Core.Services;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services
            .AddScoped<IMapeadorService, MapeadorService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IProprietarioService, ProprietarioService>()
            .AddScoped<IImovelService, ImovelService>();

        services
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        
        return services;
    }
}
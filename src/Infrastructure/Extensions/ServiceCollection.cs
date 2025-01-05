using Core.Repositories.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureDatabase(configuration)
            .AddRepositories()
            .AddInterceptors();
        
        return services;
    }
    
    private static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? 
                               configuration.GetConnectionString("DefaultConnection");
        
        Console.WriteLine(connectionString);
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IMapeadorRepository, MapeadorRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IProprietarioRepository, ProprietarioRepository>()
            .AddScoped<IImovelRepository, ImovelRepository>()
            .AddScoped<IPedidoRepository, PedidoRepository>()
            .AddScoped<IEmpresaRepository, EmpresaRepository>()
            .AddScoped<IContatoRepository, ContatoRepository>()
            .AddScoped<IAbordagemRepository, AbordagemRepository>()
            .AddScoped<IHistoricoMapeamentoRepository, HistoricoMapeamentoRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services
            .AddScoped<AuditableInterceptor>();
        
        return services;
    }
}
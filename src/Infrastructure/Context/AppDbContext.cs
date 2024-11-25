using Core.Models;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options, AuditableInterceptor auditableInterceptor) : DbContext(options)
{
    private readonly AuditableInterceptor _auditableInterceptor = auditableInterceptor;
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Mapeador> Mapeadores => Set<Mapeador>();
    public DbSet<Proprietario> Proprietarios => Set<Proprietario>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
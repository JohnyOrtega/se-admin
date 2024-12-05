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
    public DbSet<Imovel> Imoveis => Set<Imovel>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proprietario>()
            .HasMany(p => p.Imoveis)
            .WithOne(i => i.Proprietario)
            .HasForeignKey(i => i.ProprietarioId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Imovel>()
            .Property(c => c.RentValue)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Imovel>()
            .Property(c => c.SaleValue)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Imovel>()
            .Property(c => c.IptuValue)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Imovel>()
            .Property(c => c.SearchMeterage)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Imovel>()
            .Property(c => c.TotalArea)
            .HasColumnType("decimal(10,2)");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
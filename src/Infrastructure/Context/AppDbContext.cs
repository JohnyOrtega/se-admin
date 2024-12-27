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
    public DbSet<PedidoImovel> PedidoImoveis => Set<PedidoImovel>();
    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<Contato> Contatos => Set<Contato>();
    public DbSet<Abordagem> Abordagens => Set<Abordagem>();
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
            .Property(c => c.IptuAnnual)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Imovel>()
            .Property(c => c.IptuMonthly)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Imovel>()
            .Property(c => c.SearchMeterage)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Imovel>()
            .Property(c => c.TotalArea)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<PedidoImovel>(entity =>
        {
            entity.HasOne(e => e.Pedido)
                .WithMany(p => p.PedidoImoveis)
                .HasForeignKey(e => e.PedidoId);
            
            entity.HasOne(e => e.Imovel)
                .WithMany()
                .HasForeignKey(e => e.ImovelId);
            
            entity.HasOne(e => e.Proprietario)
                .WithMany()
                .HasForeignKey(e => e.ProprietarioId);
        });
        
        modelBuilder.Entity<Contato>()
            .HasOne(c => c.Empresa)
            .WithMany(e => e.Contatos)
            .HasForeignKey(c => c.EmpresaId);

        modelBuilder.Entity<Abordagem>()
            .HasOne(a => a.Contato)
            .WithMany(c => c.Abordagens)
            .HasForeignKey(a => a.ContatoId);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
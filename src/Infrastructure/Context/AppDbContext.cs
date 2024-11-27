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
    public DbSet<Cemiterio> Cemiterio => Set<Cemiterio>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cemiterio>()
            .HasOne(c => c.Proprietario)
            .WithMany()
            .HasForeignKey(c => c.ProprietarioId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Cemiterio>()
            .Property(c => c.RentValue)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Cemiterio>()
            .Property(c => c.SaleValue)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Cemiterio>()
            .Property(c => c.IptuValue)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Cemiterio>()
            .Property(c => c.SearchMeterage)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Cemiterio>()
            .Property(c => c.TotalArea)
            .HasColumnType("decimal(10,2)");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
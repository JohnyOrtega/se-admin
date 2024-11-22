using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Mapeador> Mapeadores => Set<Mapeador>();
}
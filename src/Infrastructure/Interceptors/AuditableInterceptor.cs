using System.Security.Claims;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class AuditableInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var userEmail = GetUserEmail();
        
        var entries = dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.CreatedBy).CurrentValue = userEmail;
            }

            if (entry.State != EntityState.Modified) continue;
            {
                entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.UpdatedBy).CurrentValue = userEmail;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private string GetUserEmail()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return "admin@admin.com";
        }
        
        var email = httpContext.User.FindFirstValue(ClaimTypes.Email);
        return string.IsNullOrWhiteSpace(email) ? "admin@admin.com" : email;
    }
}
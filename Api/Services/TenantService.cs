using Api.Data;
using Api.Dtos.Tenant;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class TenantService(IConfiguration configuration, TenantDbContext context, IServiceProvider serviceProvider)
{
    public async Task<Tenant> Create(CreateTenantDto createTenantDto)
    {
        string newConnectionString = null;
        if (createTenantDto.Isolated)
        {
            newConnectionString = configuration.GetConnectionString("DefaultConnection")!;
            newConnectionString = newConnectionString.Replace("MultiTenentDb", $"MultiTenentDb-{createTenantDto.Id}");
        }

        try
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            ApplicationDbContext applicationDbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.SetConnectionString(newConnectionString);
            if (applicationDbContext.Database.GetPendingMigrations().Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Applying Application Migrations for New '{createTenantDto.Name}' tenant.");
                Console.ResetColor();
                applicationDbContext.Database.Migrate();
            }
        }
        catch
        {
            throw;
        }

        var tenant = new Tenant()
        {
            Id = createTenantDto.Id,
            Name = createTenantDto.Name,
            ConnectionString = newConnectionString
        };
        await context.Tenants.AddAsync(tenant);
        await context.SaveChangesAsync();
        return tenant;
    }
}
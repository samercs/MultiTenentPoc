using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Api.Models;

public class TenantDbContextFactory: IDesignTimeDbContextFactory<TenantDbContext>
{
    public TenantDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configration.GetConnectionString("DefaultConnection");
        DbContextOptionsBuilder<TenantDbContext> optionsBuilder = new();
        _=optionsBuilder.UseSqlServer(connectionString);
        return new TenantDbContext(optionsBuilder.Options);
    }
}
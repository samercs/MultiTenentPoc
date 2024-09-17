using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class MultipleDatabaseExtention
{
    public static IServiceCollection MigrateAll(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        using var scope = serviceCollection.BuildServiceProvider().CreateScope();
        var tenantDbContext = scope.ServiceProvider.GetRequiredService<TenantDbContext>();
        if (tenantDbContext.Database.GetPendingMigrations().Any())
        {
            tenantDbContext.Database.Migrate();
        }

        var tenantsList = tenantDbContext.Tenants.ToList();
        var defaultConnection = configuration.GetConnectionString("DefaultConnection");
        foreach (var tenant in tenantsList)
        {
            string tenantConnectionString = string.IsNullOrEmpty(tenant.ConnectionString) ? defaultConnection : tenant.ConnectionString;
            using var scopeApplication = serviceCollection.BuildServiceProvider().CreateScope();
            var applicationDbContext = scopeApplication.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.SetConnectionString(tenantConnectionString);
            if (applicationDbContext.Database.GetPendingMigrations().Any())
            {
                applicationDbContext.Database.Migrate();
            }
        }

        return serviceCollection;
    }
}
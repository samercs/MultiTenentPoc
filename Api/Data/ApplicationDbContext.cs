using Api.EntityConfigrations;
using Api.Models;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApplicationDbContext: DbContext
{
    private readonly ICurrentTenantService _currentTenantService;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService) : base(options)
    {
        _currentTenantService = currentTenantService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasQueryFilter(i => i.TenantId.Equals(_currentTenantService.TenantId));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfigration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        SetTenant();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetTenant();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetTenant()
    {
        foreach (var entityEntry in ChangeTracker.Entries<IMustHaveTenantId>().ToList())
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    entityEntry.Entity.TenantId = _currentTenantService.TenantId;
                    break;
            }
        }
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
}
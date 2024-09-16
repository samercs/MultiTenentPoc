using Api.Models;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class TenantDbContext: DbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
    {
    }
    public DbSet<Tenant> Tenants => Set<Tenant>();
}
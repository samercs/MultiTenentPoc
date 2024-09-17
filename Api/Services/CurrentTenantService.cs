using Api.Data;
using Api.Exception;

namespace Api.Services;

public class CurrentTenantService(TenantDbContext context): ICurrentTenantService
{
    public string? TenantId { get; set; }
    public string? ConnectionString { get; set; }
    public async Task<bool> SetTenant(string tenantId)
    {
        //check if tenant is valid
        var tenant = await context.Tenants.FindAsync(tenantId);
        if (tenant is null)
        {
            throw new TenantNotFoundException(tenantId);
        }
        TenantId = tenantId;
        ConnectionString = tenant.ConnectionString;
        return true;
    }
}
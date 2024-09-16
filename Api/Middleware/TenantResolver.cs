using Api.Services;

namespace Api.Middleware;

public class TenantResolver(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
    {
        context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader);
        if (!string.IsNullOrEmpty(tenantFromHeader))
        {
            await currentTenantService.SetTenant(tenantFromHeader);
        }

        await next(context);
    }
}
using Api.Services;
using System.Linq;

namespace Api.Middleware;

public class TenantRouteResolver
{

    private readonly RequestDelegate _next;

    public TenantRouteResolver(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
    {
        var url = context.Request.Path;
        if (!string.IsNullOrEmpty(url))
        {
            var segmants = url.Value.Split("/")
                .Where(i => !string.IsNullOrEmpty(i))
                .ToList();
            if (segmants.Count>1)
            {
                currentTenantService.SetTenant(segmants.First());
                context.Request.Path = Path.Combine("/", string.Join("/", segmants.Skip(1)));
            }
        }
        await _next(context);
    }

    
}
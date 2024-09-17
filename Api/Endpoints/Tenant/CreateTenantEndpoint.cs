using Api.Dtos.Tenant;
using Api.Endpoints.Product;
using Api.Services;
using FastEndpoints;

namespace Api.Endpoints.Tenant;

public record CreateTenantResponse(Models.Tenant Tenant);
public class CreateTenantEndpoint(TenantService service): Endpoint<CreateTenantDto, CreateTenantResponse>
{
    public override void Configure()
    {
        Post("/tenants");
        AllowAnonymous();
    }

    public override async Task  HandleAsync(CreateTenantDto req, CancellationToken ct)
    {
        var tenant = await service.Create(req);
        Response = new CreateTenantResponse(tenant);
    }
}
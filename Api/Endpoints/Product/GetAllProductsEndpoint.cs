using Api.Data;
using Api.Dtos.Product;
using Api.Services;
using FastEndpoints;
using Mapster;

namespace Api.Endpoints.Product;


public record GetAllProductsResponse(IList<ProductDto> Products);

public class GetAllProductsEndpoint(ProductService service):EndpointWithoutRequest<GetAllProductsResponse>
{
    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task  HandleAsync(CancellationToken ct)
    {
        var products = await service.GetAll();
        Response = new GetAllProductsResponse(products);
    }
}
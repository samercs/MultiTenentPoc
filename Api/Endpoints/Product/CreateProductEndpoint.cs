using Api.Data;
using Api.Dtos.Product;
using Api.Services;
using FastEndpoints;
using Mapster;

namespace Api.Endpoints.Product;

public record CreateProductRequest(CreateProductDto Products);
public record CreateProductResponse(ProductDto Products);


public class CreateProductEndpoint(ProductService service):Endpoint<CreateProductRequest,CreateProductResponse>
{
    public override void Configure()
    {
        Post("/products");
        AllowAnonymous();
    }

    public override async Task  HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        var result = await service.Create(req.Products);
        Response = new CreateProductResponse(result);
    }
}
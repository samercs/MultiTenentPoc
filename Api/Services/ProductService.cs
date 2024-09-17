using Api.Data;
using Api.Dtos.Product;
using Api.Models;
using Mapster;

namespace Api.Services;

public class ProductService(ApplicationDbContext context)
{
    public async Task<IList<ProductDto>> GetAll()
    {
        var listOfProducts = context.Products.ToList();
        return listOfProducts.Adapt<List<ProductDto>>();
    }

    public async Task<ProductDto> Create(CreateProductDto dto)
    {
        var product = dto.Adapt<Product>();
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return product.Adapt<ProductDto>();
    }
}
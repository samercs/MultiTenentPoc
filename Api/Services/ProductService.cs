using Api.Data;
using Api.Dtos.Product;
using Mapster;

namespace Api.Services;

public class ProductService(ApplicationDbContext context)
{
    public async Task<IList<ProductDto>> GetAll()
    {
        var listOfProducts = context.Products.ToList();
        return listOfProducts.Adapt<List<ProductDto>>();
    }
}
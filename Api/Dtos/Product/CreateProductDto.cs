namespace Api.Dtos.Product;

public class CreateProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Supplier { get; set; }
}
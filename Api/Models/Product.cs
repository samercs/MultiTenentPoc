namespace Api.Models;

public class Product: IMustHaveTenantId
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string TenantId { get; set; }

    public virtual Tenant Tenant { get; set; }
}
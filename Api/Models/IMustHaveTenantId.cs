namespace Api.Models;

public interface IMustHaveTenantId
{
    public string TenantId { get; set; }
}
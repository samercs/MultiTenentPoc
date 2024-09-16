namespace Api.Exception;

public class TenantNotFoundException: System.Exception
{
    public TenantNotFoundException(string tenantName) : base($"Tenant not found- Tenant name ({tenantName})")
    {

    }
}
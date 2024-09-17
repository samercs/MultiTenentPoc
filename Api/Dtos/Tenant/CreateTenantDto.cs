namespace Api.Dtos.Tenant;

public class CreateTenantDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Isolated { get; set; }
}
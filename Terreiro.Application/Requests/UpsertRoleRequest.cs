namespace Terreiro.Application.Requests;

public class UpsertRoleRequest(string name, string? description)
{
    public string Name { get; } = name;
    public string? Description { get; set; } = description;
}

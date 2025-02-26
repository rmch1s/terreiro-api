namespace Terreiro.Domain.Entities;

public class Role(string name, string? description = null) : Entity
{
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description;

    public ICollection<UserRole> UserRoles { get; } = [];
    public ICollection<User> Users { get; } = [];

    public void SetName(string name) => Name = name;
    public void SetDescription(string description) => Description = description;
}

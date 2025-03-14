using Terreiro.Domain.Entities.Base;

namespace Terreiro.Domain.Entities;

public class Role(string name, string? description = null) : BaseEntity
{
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description;

    public virtual ICollection<User> Users { get; } = [];

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}

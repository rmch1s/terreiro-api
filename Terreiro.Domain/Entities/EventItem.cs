using Terreiro.Domain.Entities.Base;

namespace Terreiro.Domain.Entities;

public class EventItem(string name, int quantity, int eventId) : BaseEntity
{
    public string Name { get; private set; } = name;
    public int Quantity { get; private set; } = quantity;
    public int EventId { get; } = eventId;

    public virtual Event Event { get; } = default!;
    public virtual ICollection<User> Users { get; } = [];

    public void Update(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }
}

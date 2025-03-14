using Terreiro.Domain.Entities.Base;

namespace Terreiro.Domain.Entities;

public class EventItem : BaseEntity
{
    private EventItem() { }

    public EventItem(string name, int quantity, int eventId)
    {
        Name = name;
        Quantity = quantity;
        EventId = eventId;
    }

    public string Name { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public int EventId { get; }

    public virtual Event Event { get; } = default!;
    public ICollection<UserEventItem> UserEventItems { get; } = [];
    public ICollection<User> Users { get; } = [];

    public void Update(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }
}

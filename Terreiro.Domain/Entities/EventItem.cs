namespace Terreiro.Domain.Entities;

public class EventItem(string name, int quantity, int eventId) : Entity
{
    public string Name { get; private set; } = name;
    public int Quantity { get; private set; } = quantity;
    public int EventId { get; } = eventId;

    public virtual Event Event { get; } = default!;
    public ICollection<UserEventItem> UserEventItems { get; } = [];
    public ICollection<User> Users { get; } = [];

    public void SetName(string name) => Name = name;
    public void SetQuantity(int quantity) => Quantity = quantity;
}

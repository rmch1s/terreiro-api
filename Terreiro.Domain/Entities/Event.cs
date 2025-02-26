using Terreiro.Domain.ValueObjects;

namespace Terreiro.Domain.Entities;

public class Event : Entity
{
    private Event() { }

    public Event(string name, Period period, List<EventItem> items, string? description = null)
    {
        Name = name;
        Period = period;
        Items = items;
        Description = description;
    }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Period Period { get; private set; } = default!;

    public ICollection<EventItem> Items { get; } = [];
    public ICollection<UserEvent> UserEvents { get; } = [];
    public ICollection<User> Users { get; } = [];
}

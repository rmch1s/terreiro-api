using Terreiro.Domain.Entities.Base;

namespace Terreiro.Domain.Entities;

public class UserEvent : Entity
{
    private UserEvent() { }

    public UserEvent(int userId, int eventId)
    {
        UserId = userId;
        EventId = eventId;
    }

    public int UserId { get; }
    public int EventId { get; }

    public virtual User User { get; } = default!;
    public virtual Event Event { get; } = default!;
}

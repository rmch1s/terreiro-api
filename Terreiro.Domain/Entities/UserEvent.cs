namespace Terreiro.Domain.Entities;

public class UserEvent(int userId, int eventId) : Entity
{
    public int UserId { get; } = userId;
    public int EventId { get; } = eventId;

    public virtual User User { get; } = default!;
    public virtual Event Event { get; } = default!;
}

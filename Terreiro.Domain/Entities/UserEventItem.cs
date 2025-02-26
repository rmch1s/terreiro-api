namespace Terreiro.Domain.Entities;

public class UserEventItem(int userId, int eventItemId) : Entity
{

    public int UserId { get; } = userId;
    public int EventItemId { get; } = eventItemId;

    public virtual User User { get; } = default!;
    public virtual EventItem EventItem { get; } = default!;
}

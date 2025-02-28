﻿namespace Terreiro.Domain.Entities;

public class UserEventItem
{
    private UserEventItem() { }

    public UserEventItem(int userId, int eventItemId)
    {
        UserId = userId;
        EventItemId = eventItemId;
    }

    public int UserId { get; }
    public int EventItemId { get; }

    public virtual User User { get; } = default!;
    public virtual EventItem EventItem { get; } = default!;
}

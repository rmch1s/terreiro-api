using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpsertUserEventItem;

public interface IUpsertUserEventItemService
{
    Task<(int, EventItem?)> Upsert(User user, EventItem eventItem);
}

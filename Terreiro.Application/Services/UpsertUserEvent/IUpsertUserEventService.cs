using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpsertUserEvent;

public interface IUpsertUserEventService
{
    Task<(int, Event?)> Upsert(User user, Event @event);
}

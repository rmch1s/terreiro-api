using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpdateUserEvent;

public interface IUpdateUserEventService
{
    Task<(int, Event?)> Update(User user, Event @event);
}

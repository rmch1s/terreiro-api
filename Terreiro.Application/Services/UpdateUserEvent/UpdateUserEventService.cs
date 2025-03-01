using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpdateUserEvent;

public class UpdateUserEventService(IUserEventRepository userEventRepository) : IUpdateUserEventService
{
    public async Task<(int, Event?)> Update(User user, Event @event)
    {
        var userEvent = new UserEvent(user.Id, @event.Id);
        var isAdding = !user.Events.Any(e => e.Id == @event.Id);

        var rowsAffected = isAdding
            ? await userEventRepository.Add(userEvent)
            : await userEventRepository.Delete(userEvent);

        return (rowsAffected, isAdding ? @event : null);
    }
}

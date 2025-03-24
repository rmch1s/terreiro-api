using Terreiro.Application.Exceptions;
using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpsertUserEvent;

internal class UpsertUserEventService(IUserEventRepository userEventRepository) : IUpsertUserEventService
{
    public async Task<(int, Event?)> Upsert(User user, Event @event)
    {
        if (user is null || @event is null)
            throw new NullEntityExecption();

        var userEvent = new UserEvent(user.Id, @event.Id);
        var isAdding = !user.Events.Any(e => e.Id == @event.Id);

        var rowsAffected = isAdding
            ? await userEventRepository.Add(userEvent)
            : await userEventRepository.Delete(userEvent);

        return (rowsAffected, isAdding ? @event : null);
    }
}

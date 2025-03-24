using Terreiro.Application.Exceptions;
using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpsertUserEventItem;

internal class UpsertUserEventItemService(IUserEventItemRepository userEventItemRepository) : IUpsertUserEventItemService
{
    public async Task<(int, EventItem?)> Upsert(User user, EventItem eventItem)
    {
        if (user is null || eventItem is null)
            throw new NullEntityExecption();

        var userEventItem = new UserEventItem(user.Id, eventItem.Id);
        var isAdding = !user.EventItems.Any(e => e.Id == eventItem.Id);

        var rowsAffected = isAdding
            ? await userEventItemRepository.Add(userEventItem)
            : await userEventItemRepository.Delete(userEventItem);

        return (rowsAffected, isAdding ? eventItem : null);
    }
}

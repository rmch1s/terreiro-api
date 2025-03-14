using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpdateUserEventItem;

internal class UpdateUserEventItemService(IUserEventItemRepository userEventItemRepository) : IUpdateUserEventItemService
{
    public async Task<(int, EventItem?)> Update(User user, EventItem eventItem)
    {
        var userEventItem = new UserEventItem(user.Id, eventItem.Id);
        var isAdding = !user.EventItems.Any(e => e.Id == eventItem.Id);

        var rowsAffected = isAdding
            ? await userEventItemRepository.Add(userEventItem)
            : await userEventItemRepository.Delete(userEventItem);

        return (rowsAffected, isAdding ? eventItem : null);
    }
}

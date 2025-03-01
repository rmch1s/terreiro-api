using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpdateUserEventItem;

public interface IUpdateUserEventItemService
{
    Task<(int, EventItem?)> Update(User user, EventItem eventItem);
}

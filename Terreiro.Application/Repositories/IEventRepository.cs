using Terreiro.Domain.Entities;

namespace Terreiro.Application.Repositories;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<IEnumerable<Event>> Get(DateTime? startDate, DateTime? endDate);
}

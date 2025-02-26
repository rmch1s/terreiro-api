using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public class EventItemRepository(TerreiroDbContext db) : Repository<EventItem>(db), IEventItemRepository;

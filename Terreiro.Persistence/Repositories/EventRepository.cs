using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public class EventRepository(TerreiroDbContext db) : Repository<Event>(db), IEventRepository;
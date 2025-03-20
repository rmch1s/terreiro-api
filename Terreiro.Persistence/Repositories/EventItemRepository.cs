using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Configurations;

namespace Terreiro.Persistence.Repositories;

internal class EventItemRepository(TerreiroDbContext db) : RepositoryBase<EventItem>(db), IEventItemRepository;

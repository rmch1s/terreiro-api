using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Configurations;

namespace Terreiro.Persistence.Repositories;

internal class UserEventItemRepository(TerreiroDbContext db) : Repository<UserEventItem>(db), IUserEventItemRepository;

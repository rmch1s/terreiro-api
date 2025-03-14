using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Configurations;

namespace Terreiro.Persistence.Repositories;

internal class UserEventRepository(TerreiroDbContext db) : Repository<UserEvent>(db), IUserEventRepository;
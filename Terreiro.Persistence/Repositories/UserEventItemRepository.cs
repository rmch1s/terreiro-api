using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public class UserEventItemRepository(TerreiroDbContext db) : Repository<UserEventItem>(db), IUserEventItemRepository;

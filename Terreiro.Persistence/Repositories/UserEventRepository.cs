using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public class UserEventRepository(TerreiroDbContext db) : Repository<UserEvent>(db), IUserEventRepository;
using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public class UserRoleRepository(TerreiroDbContext db) : Repository<UserRole>(db), IUserRoleRepository;

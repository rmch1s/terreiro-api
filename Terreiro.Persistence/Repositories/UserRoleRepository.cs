using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Configurations;

namespace Terreiro.Persistence.Repositories;

internal class UserRoleRepository(TerreiroDbContext db) : Repository<UserRole>(db), IUserRoleRepository;

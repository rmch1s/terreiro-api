using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public class RoleRepository(TerreiroDbContext db) : Repository<Role>(db), IRoleRepository;

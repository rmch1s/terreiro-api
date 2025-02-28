using Microsoft.EntityFrameworkCore;
using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public class RoleRepository(TerreiroDbContext db) : Repository<Role>(db), IRoleRepository
{
    public async Task<IEnumerable<Role>> Get(IEnumerable<int> ids) =>
        await dbSet.Where(r => ids.Contains(r.Id)).ToListAsync();
}

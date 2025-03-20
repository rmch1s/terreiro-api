using Microsoft.EntityFrameworkCore;
using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Configurations;

namespace Terreiro.Persistence.Repositories;

internal class RoleRepository(TerreiroDbContext db) : RepositoryBase<Role>(db), IRoleRepository
{
    public async Task<IEnumerable<Role>> Get(IEnumerable<int> ids) =>
        await dbSet
            .AsNoTracking()
            .Where(r => ids.Contains(r.Id) && !r.DeletedAt.HasValue)
            .ToListAsync();
}

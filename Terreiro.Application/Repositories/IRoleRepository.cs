using Terreiro.Domain.Entities;

namespace Terreiro.Application.Repositories;

public interface IRoleRepository : IRepositoryBase<Role>
{
    Task<IEnumerable<Role>> Get(IEnumerable<int> ids);
}

using Terreiro.Domain.Entities;

namespace Terreiro.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<int> UpdatePin(User user);
    Task<int> UpdateRoles(User user);
}
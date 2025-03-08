using Terreiro.Domain.Entities;

namespace Terreiro.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<int> UpdatePin(User user);
    Task<User?> GetByCpf(string cpf);
}
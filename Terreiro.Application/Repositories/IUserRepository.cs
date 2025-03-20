using Terreiro.Domain.Entities;

namespace Terreiro.Application.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<int> UpdatePin(User user);
    Task<User?> GetByCpf(string cpf);
}
using System.Linq.Expressions;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> Get();
    Task<T?> Get(int id, params Expression<Func<T, object>>[] includes);
    Task<int> Add(T entity);
    Task<int> Update(T entity);
    Task<int> Delete(T entity);
}

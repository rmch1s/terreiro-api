using System.Linq.Expressions;
using Terreiro.Domain.Entities.Base;

namespace Terreiro.Application.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> Get();
    Task<T?> GetFirst(int id = 0, params Expression<Func<T, object>>[] includes);
    Task<int> Add(T entity);
    Task<int> Add(IEnumerable<T> entities);
    Task<int> Update(T entity);
    Task<int> Delete(T entity);
}

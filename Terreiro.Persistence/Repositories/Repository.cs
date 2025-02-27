using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public abstract class Repository<T>(TerreiroDbContext db) where T : Entity
{
    protected TerreiroDbContext db = db;
    protected DbSet<T> dbSet = db.Set<T>();

    public async Task<IEnumerable<T>> Get() =>
        await dbSet.AsNoTracking().Where(e => !e.DeletedAt.HasValue).ToListAsync();

    public async Task<T?> Get(int id, params Expression<Func<T, object>>[] includes)
    {
        var query = dbSet.AsQueryable();

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync(e => e.Id == id && !e.DeletedAt.HasValue);
    }

    public async Task<int> Add(T entity)
    {
        await dbSet.AddAsync(entity);

        return await db.SaveChangesAsync();
    }

    public async Task<int> Add(IEnumerable<T> entities)
    {
        await dbSet.AddRangeAsync(entities);

        return await db.SaveChangesAsync();
    }

    public async Task<int> Update(T entity)
    {
        dbSet.Update(entity);

        return await db.SaveChangesAsync();
    }

    public async Task<int> Delete(T entity)
    {
        entity.SetDeletedAt();

        db.Attach(entity);

        db.Entry(entity).Property(p => p.DeletedAt).IsModified = true;

        return await db.SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Terreiro.Domain.Entities.Base;
using Terreiro.Persistence.Configurations;

namespace Terreiro.Persistence.Repositories;

internal abstract class Repository<T>(TerreiroDbContext db) where T : Entity
{
    protected readonly TerreiroDbContext db = db;
    protected readonly DbSet<T> dbSet = db.Set<T>();

    public async Task<IEnumerable<T>> Get()
    {
        var query = dbSet.AsNoTracking();

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
            query = query.Where(e => !EF.Property<DateTime?>(e, "DeletedAt").HasValue);

        return await query.ToListAsync();
    }

    public async Task<T?> GetFirst(int id, params Expression<Func<T, object>>[] includes)
    {
        var query = dbSet.AsNoTracking();

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => EF.Property<int>(e, "Id").Equals(id));
            query = query.Where(e => !EF.Property<DateTime?>(e, "DeletedAt").HasValue);
        }

        return await query.FirstOrDefaultAsync();
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
        db.Attach(entity);

        dbSet.Update(entity);

        return await db.SaveChangesAsync();
    }

    public async Task<int> Delete(T entity)
    {
        db.Attach(entity);

        if (entity is BaseEntity baseEntity)
            db.Entry(baseEntity).Property(p => p.DeletedAt).IsModified = true;
        else
            dbSet.Remove(entity);

        return await db.SaveChangesAsync();
    }
}

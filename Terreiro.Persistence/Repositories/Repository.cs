using Microsoft.EntityFrameworkCore;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public abstract class Repository<T>(TerreiroDbContext db) where T : Entity
{
    private DbSet<T> _dbSet = db.Set<T>();

    public async Task<IEnumerable<T>> Get() =>
        await _dbSet.AsNoTracking().Where(e => !e.DeletedAt.HasValue).ToListAsync();

    public async Task<T?> Get(int id) =>
        await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.DeletedAt.HasValue);

    public async Task<int> Add(T entity)
    {
        await _dbSet.AddAsync(entity);

        return await db.SaveChangesAsync();
    }

    public async Task<int> Update(T entity)
    {
        _dbSet.Update(entity);

        return await db.SaveChangesAsync();
    }

    public async Task<int> Delete(T entity)
    {
        db.Attach(entity);

        db.Entry(entity).Property(p => p.DeletedAt).IsModified = true;

        return await db.SaveChangesAsync();
    }
}

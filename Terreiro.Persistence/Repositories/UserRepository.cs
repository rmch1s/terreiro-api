using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public class UserRepository(TerreiroDbContext db) : Repository<User>(db), IUserRepository
{
    public async Task<int> UpdatePin(User user)
    {
        db.Attach(user);

        db.Entry(user).Property(p => p.PIN).IsModified = true;

        return await db.SaveChangesAsync();
    }
}
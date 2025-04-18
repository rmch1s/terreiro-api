﻿using Microsoft.EntityFrameworkCore;
using Terreiro.Application.Exceptions;
using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Configurations;

namespace Terreiro.Persistence.Repositories;

internal class UserRepository(TerreiroDbContext db) : RepositoryBase<User>(db), IUserRepository
{
    public async Task<User?> GetByCpf(string cpf) =>
        await dbSet
            .AsNoTracking()
            .Include(u => u.Roles.Where(r => !r.DeletedAt.HasValue))
            .FirstOrDefaultAsync(u => u.CPF.Equals(cpf) && !u.DeletedAt.HasValue);

    public async Task<int> UpdatePin(User user)
    {
        if (user is null)
            throw new NullEntityExecption();

        db.Attach(user);

        db.Entry(user).Property(p => p.PIN).IsModified = true;

        return await db.SaveChangesAsync();
    }
}
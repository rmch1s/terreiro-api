﻿using Terreiro.Application.Exceptions;
using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpsertUserRole;

internal class UpsertUserRoleService(IUserRoleRepository userRoleRepository) : IUpsertUserRoleService
{
    public async Task<(int, Role?)> Upsert(User user, Role role)
    {
        if (user is null || role is null)
            throw new NullEntityExecption();

        var userRole = new UserRole(user.Id, role.Id);
        var isAdding = !user.Roles.Any(e => e.Id == role.Id);

        var rowsAffected = isAdding
            ? await userRoleRepository.Add(userRole)
            : await userRoleRepository.Delete(userRole);

        return (rowsAffected, isAdding ? role : null);
    }
}

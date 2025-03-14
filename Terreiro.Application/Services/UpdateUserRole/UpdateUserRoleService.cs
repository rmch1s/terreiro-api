using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpdateUserRole;

internal class UpdateUserRoleService(IUserRoleRepository userRoleRepository) : IUpdateUserRoleService
{
    public async Task<(int, Role?)> Update(User user, Role role)
    {
        var userRole = new UserRole(user.Id, role.Id);
        var isAdding = !user.Roles.Any(e => e.Id == role.Id);

        var rowsAffected = isAdding
            ? await userRoleRepository.Add(userRole)
            : await userRoleRepository.Delete(userRole);

        return (rowsAffected, isAdding ? role : null);
    }
}
